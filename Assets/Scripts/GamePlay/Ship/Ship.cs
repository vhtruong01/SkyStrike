using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public sealed class Ship : Commander, IShipComponent, IEntity, ICollector
    {
        public static Vector3 pos { get; private set; }
        private readonly EndGameEventData endGameEventData = new();
        [field: SerializeField] public ShipData shipData { get; set; }
        private Rigidbody2D rigi;
        private bool isEndGame;

        public void OnEnable()
        {
            EventManager.Subscribe(EEventType.StartGame, MoveAndFire);
            EventManager.Subscribe(EEventType.WinGame, WinGame);
            EventManager.Subscribe(EEventType.LoseGame, LoseGame);
            EventManager.Subscribe<EnemyDieEventData>(Upgrade);
        }
        public void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.StartGame, MoveAndFire);
            EventManager.Unsubscribe(EEventType.WinGame, WinGame);
            EventManager.Unsubscribe(EEventType.LoseGame, LoseGame);
            EventManager.Unsubscribe<EnemyDieEventData>(Upgrade);
        }
        public override void Init()
            => rigi = GetComponent<Rigidbody2D>();
        public void Start()
        {
            animator.GetAnimation(EAnimationType.Invincibility)
                .SetStartedAction(Disappear)
                .SetFinishedAction(Appear)
                .SetDuration(shipData.invincibleTime);
        }
        private IEnumerator Recover()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                shipData.energy += shipData.recoverSpeed;
            }
        }
        private void Update()
            => pos = entity.transform.position;
        private void Upgrade(EnemyDieEventData eventData)
        {
            shipData.energy += eventData.energy;
            shipData.score += eventData.score;
            shipData.exp += eventData.exp;
        }
        private void MoveAndFire()
        {
            StartCoroutine(Recover());
            StartCoroutine(movement.Travel(1));
            spawner.Spawn();
        }
        protected override void SetData()
        {
            shipData.ResetData();
            foreach (var comp in entityObject.GetComponentsInChildren<IShipComponent>(true))
                comp.shipData = shipData;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item") && collision.TryGetComponent<IItem>(out var item))
            {
                item.Interact(this);
                return;
            }
            if (collision.TryGetComponent<IDamager>(out var damager))
                damager.OnHit(this);
        }
        public override void Interrupt()
        {
            rigi.simulated = false;
            shipData.invincibility = true;
            StopAllCoroutines();
        }
        public void Collect(EItem item)
        {
            if (item != EItem.Star1 && item != EItem.Star5)
                animator.GetAnimation(EAnimationType.Highlight).Restart();
            shipData.CollectItem(item);
        }
        public bool TakeDamage(IDamager damager)
        {
            int dmg = damager.GetDamage();
            if (shipData.invincibility || dmg == 0) return false;
            if (!shipData.shield)
            {
                shipData.hp -= dmg;
                if (shipData.hp == 1)
                    SoundManager.PlaySound(ESound.LowHp);
                if (shipData.hp <= 0)
                    Die();
                else
                {
                    SoundManager.PlaySound(ESound.HpWarning);
                    EventManager.Active(EEventType.ShakeScreen);
                    animator.GetAnimation(EAnimationType.Damaged).Play();
                    animator.GetAnimation(EAnimationType.Invincibility).Play();
                }
            }
            return true;
        }
        public void Appear()
        {
            shipData.invincibility = false;
            spawner.Spawn();
        }
        public void Disappear()
        {
            shipData.invincibility = true;
            spawner.Stop();
        }
        public void Die()
            => EventManager.Active(EEventType.LoseGame);
        private void WinGame()
        {
            SoundManager.PlaySound(ESound.StageComplete);
            SoundManager.PlaySound(ESound.Win);
            InterruptAllComponents();
            EndGame(true);
        }
        private void LoseGame()
        {
            SoundManager.PlaySound(ESound.GameOver);
            SoundManager.PlaySound(ESound.Lose);
            InterruptAllComponents();
            animator.GetAnimation(EAnimationType.Destruction)
                    .SetFinishedAction(() => entityObject.SetActive(false))
                    .Play();
            EndGame(false);
        }
        private void EndGame(bool state)
        {
            if (isEndGame) return;
            isEndGame = true;
            endGameEventData.isWin = state;
            endGameEventData.score = shipData.score;
            endGameEventData.star = shipData.star;
            EventManager.Active(endGameEventData);
            SoundManager.StopBgm();
        }
    }
}