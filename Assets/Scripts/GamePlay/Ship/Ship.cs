using SkyStrike.UI;
using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public sealed class Ship : Commander, IShipComponent, IEntity, ICollector
    {
        public static Vector3 pos { get; private set; }
        private readonly NotiEventData notiEventData = new();
        [field: SerializeField] public ShipData shipData { get; set; }
        private Rigidbody2D rigi;

        public void OnEnable()
        {
            EventManager.Subscribe(EEventType.StartGame, MoveAndFire);
            EventManager.Subscribe<EnemyDieEventData>(Upgrade);
        }
        public void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.StartGame, MoveAndFire);
            EventManager.Unsubscribe<EnemyDieEventData>(Upgrade);
        }
        public override void Init()
            => rigi = GetComponent<Rigidbody2D>();
        public void Start()
            => StartCoroutine(Recover());
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
            StopAllCoroutines();
        }
        public void Collect(EItem item)
        {
            if (item != EItem.Star1 && item != EItem.Star5)
            {
                animator.GetAnimation(EAnimationType.Highlight).Restart();
                notiEventData.notiType = ENoti.Safe;
                notiEventData.message = item.ToString();
                EventManager.ActiveUIEvent(notiEventData);
            }
            shipData.CollectItem(item);
        }
        public bool TakeDamage(IDamager damager)
        {
            //
            if (shipData.invincibility) return false;
            if (!shipData.shield)
            {
                shipData.hp -= damager.GetDamage();
                if (shipData.hp <= 0)
                    Die();
                else
                {
                    EventManager.Active(EEventType.ShakeScreen);
                    animator.GetAnimation(EAnimationType.Damaged).Play();
                    //invisibility
                }
            }
            return true;
        }
        public void Disappear()
        {
            //win
        }
        public void Die()
        {
            //InterruptAllComponents();
            animator.GetAnimation(EAnimationType.Destruction).Play();
            //loss
        }
    }
}