using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class Enemy : PoolableObject<EnemyData>, IEnemyComponent, IEntity
    {
        private readonly ItemData.ItemEventData itemEventData = new();
        private Rigidbody2D rigi;
        public IEntity entity { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Init()
            => rigi = GetComponent<Rigidbody2D>();
        public override void Refresh()
        {
            rigi.simulated = true;
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.color = data.metaData.color;
            col2D.size = data.metaData.sprite.bounds.size / 2;
            spriteRenderer.color = data.metaData.color;
            entity.transform.localScale = Vector3.one * data.size;
            if (data.dropItemType != EItem.None && data.metaData.CanHighlight())
                notifyAction?.Invoke(EEntityAction.Highlight);
        }
        public bool TakeDamage(int dmg)
        {
            if (data.isDie || data.isImmortal) return false;
            if (!data.shield)
            {
                data.hp -= dmg;
                if (data.hp <= 0)
                    Die();
                else notifyAction?.Invoke(EEntityAction.TakeDamage);
            }
            return true;
        }
        public void Die()
        {
            Enable(false);
            data.isDie = true;
            notifyAction?.Invoke(EEntityAction.Die);
        }
        public void DropItemAndDisappear()
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
        public void Strike(float delay)
            => StartCoroutine(Prepare(delay));
        private IEnumerator Prepare(float delay)
        {
            Enable(false);
            yield return new WaitForSeconds(delay);
            Enable(true);
            notifyAction?.Invoke(EEntityAction.Arrive);
        }
        public void Interrupt() => rigi.simulated = false;
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