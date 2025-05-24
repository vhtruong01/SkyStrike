using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class Enemy : ObjectEntity<EnemyMetaData>, IEnemyComponent, IEntity, IDamager
    {
        private readonly BossEventData bossEventData = new();
        private readonly EnemyDieEventData enemyDieEventData = new();
        private readonly ItemEventData itemEventData = new();
        private readonly DamageVisualizerEventData damageVisualizer = new();
        [SerializeField] private AlphaValueAnimation damagedAnimation;
        [SerializeField] private SpriteAnimation destructionAnimation;
        private Rigidbody2D rigi;
        private EnemyCommander commander;
        private IAnimator animator;
        public IObject entity { get; set; }
        public EnemyData enemyData { get; set; }
        public EDamageType damageType => EDamageType.Slashing;
        public int GetDamage() => 1;
        public bool isDie => isActive;

        public void Init()
        {
            rigi = GetComponent<Rigidbody2D>();
            commander = GetComponent<EnemyCommander>();
            animator = commander.animator;
        }
        public void RefreshData()
        {
            rigi.simulated = true;
            damagedAnimation.SetData(spriteRenderer.sprite);
            destructionAnimation.SetData(enemyData.metaData.destructionSprites);
            if (enemyData.dropItemType != EItem.None && enemyData.metaData.CanHighlight())
                animator.GetAnimation(EAnimationType.Highlight).Play();
            destructionAnimation.SetFinishedAction(DropItemAndDisapear);
            if (enemyData.metaData.type == EnemyType.Boss)
            {
                bossEventData.bossData = enemyData;
                EventManager.ActiveUIEvent(bossEventData);
            }
        }
        protected override IEnumerator Prepare(float delay)
        {
            Enable(false);
            yield return new WaitForSeconds(delay);
            Enable(true);
            commander.Reload();
        }
        public void Interrupt()
            => rigi.simulated = false;
        public bool TakeDamage(IDamager damager)
        {
            if (!isActive || enemyData.isImmortal) return false;
            if (!enemyData.shield || damager.damageType != EDamageType.Normal)
            {
                enemyData.hp -= damager.GetDamage();
                DisplayDamage(damager.GetDamage(), damager.damageType);
                if (enemyData.hp <= 0)
                    Die();
                else
                {
                    animator.GetAnimation(EAnimationType.Damaged).Play();
                    SoundManager.PlaySound(ESound.EnemyHit);
                }
            }
            return true;
        }
        private void DisplayDamage(int damage, EDamageType damageType)
        {
            damageVisualizer.damage = damage;
            damageVisualizer.damageType = damageType;
            damageVisualizer.position = transform.position + Random.insideUnitSphere * 0.5f;
            EventManager.ActiveUIEvent(damageVisualizer);
        }
        public void Die()
        {
            Enable(false);
            enemyDieEventData.score = data.metaData.score;
            enemyDieEventData.exp = data.metaData.exp;
            enemyDieEventData.energy = data.metaData.energy;
            SoundManager.PlaySound(enemyData.metaData.dieSoundType);
            EventManager.Active(enemyDieEventData);
            commander.InterruptAllComponents();
            destructionAnimation.Play();
        }
        public void DropItemAndDisapear()
        {
            itemEventData.position = entity.position;
            if (data.metaData.star != 0)
            {
                int star5 = data.metaData.star / 10;
                int star1 = data.metaData.star - star5 * 5;
                DropItem(EItem.Star5, star5);
                DropItem(EItem.Star1, star1);
            }
            if (enemyData.dropItemType != EItem.None)
                DropItem(enemyData.dropItemType, 1);
            Disappear();
        }
        private void DropItem(EItem itemType, int amount)
        {
            itemEventData.itemType = itemType;
            itemEventData.amount = amount;
            EventManager.Active(itemEventData);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamager>(out var bullet))
            {
                bullet.OnHit(this);
                return;
            }
        }
        public void AfterHit()
        {
            if (data.metaData.isWeakEnemy)
                Die();
        }
    }
}