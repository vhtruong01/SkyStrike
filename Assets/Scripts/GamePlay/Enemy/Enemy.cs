using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class Enemy : ObjectEntity<EnemyMetaData>, IEnemyComponent, IEntity
    {
        private readonly BossEventData bossEventData = new();
        [SerializeField] private MaterialAlphaAnimation damagedAnimation;
        [SerializeField] private SpriteAnimation destructionAnimation;
        private readonly ItemEventData itemEventData = new();
        private readonly DamageVisualizerEventData damageVisualizer = new();
        private Rigidbody2D rigi;
        private EnemyCommander commander;
        private IAnimator animator;
        public IObject entity { get; set; }
        public EnemyData enemyData { get; set; }

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
                EventManager.Active(bossEventData);
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
            if (!enemyData.shield || damager.damageType == EDamageType.Slashing)
            {
                enemyData.hp -= damager.GetDamage();
                DisplayDamage(damager.GetDamage(), damager.damageType);
                if (enemyData.hp <= 0)
                    Die();
                else animator.GetAnimation(EAnimationType.Damaged).Play();
            }
            return true;
        }
        private void DisplayDamage(int damage, EDamageType damageType)
        {
            damageVisualizer.damage = damage;
            damageVisualizer.damageType = damageType;
            damageVisualizer.position = transform.position + Random.insideUnitSphere * 0.5f;
            EventManager.Active(damageVisualizer);
        }
        public void Die()
        {
            Enable(false);
            commander.InterruptAllComponents();
            animator.GetAnimation(EAnimationType.Destruction).Play();
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
            if (collision.CompareTag("ShipBullet") && collision.TryGetComponent<IDamager>(out var bullet))
            {
                bullet.OnHit(this);
                return;
            }
        }
    }
}