using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyCommander))]
    public class Enemy : PoolableObject<EnemyData>, IEnemyComponent, IEntity
    {
        [SerializeField] private MaterialAlphaAnimation damagedAnimation;
        [SerializeField] private SpriteAnimation destructionAnimation;
        private readonly ItemData.ItemEventData itemEventData = new();
        private Rigidbody2D rigi;
        private EnemyCommander commander;
        private IAnimator animator;
        public IEntity entity { get; set; }
        public EnemyData enemyData { get; set; }

        public void Init()
        {
            rigi = GetComponent<Rigidbody2D>();
            commander = GetComponent<EnemyCommander>();
            animator = commander.animator;
        }
        public void UpdateData()
        {
            rigi.simulated = true;
            damagedAnimation.SetData(spriteRenderer.sprite);
            destructionAnimation.SetData(enemyData.metaData.destructionSprites);
            if (enemyData.dropItemType != EItem.None && enemyData.metaData.CanHighlight())
                animator.GetAnimation(EAnimationType.Highlight).Play();
            destructionAnimation.SetFinishedAction(DropItemAndDisapear);
        }
        public override void Refresh()
        {
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.color = data.metaData.color;
            col2D.size = data.metaData.sprite.bounds.size / 2;
            spriteRenderer.color = data.metaData.color;
            entity.transform.localScale = Vector3.one * data.size;
        }
        public void Launch(float delay)
            => StartCoroutine(Prepare(delay));
        private IEnumerator Prepare(float delay)
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
            if (!data.shield)
            {
                data.hp -= damager.GetDamage();
                if (data.hp <= 0)
                    Die();
                else animator.GetAnimation(EAnimationType.Damaged).Play();
            }
            return true;
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
            if (data.dropItemType != EItem.None)
                DropItem(data.dropItemType, 1);
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