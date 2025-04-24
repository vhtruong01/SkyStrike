using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyData))]
    public class Enemy : PoolableObject<EnemyData>, IEnemyComponent, IEntity
    {
        private Rigidbody2D rigi;
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public override void Awake()
        {
            base.Awake();
            rigi = GetComponent<Rigidbody2D>();
        }
        public override void Refresh()
        {
            rigi.simulated = true;
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.color = data.metaData.color;
            col2D.size = data.metaData.sprite.bounds.size / 2;
            spriteRenderer.color = data.metaData.color;
            transform.localScale = Vector3.one * data.size;
            if (data.dropItemType != EItem.None && data.metaData.CanHighlight())
                notifyAction?.Invoke(EEntityAction.Highlight);
        }
        public void Strike(float delay)
            => StartCoroutine(Prepare(delay));
        public bool TakeDamage(int dmg)
        {
            if (data.isDie || data.isImmortal) return false;
            if (!data.shield)
            {
                data.hp -= dmg;
                if (data.hp <= 0)
                    Die();
                else notifyAction?.Invoke(EEntityAction.TakeDmg);
            }
            return true;
        }
        public void Die()
        {
            data.isDie = true;
            notifyAction?.Invoke(EEntityAction.Die);
        }
        public void DropItemAndDisappear()
        {
            EventManager.DropStar(transform.position, data.metaData.star);
            if (data.dropItemType != EItem.None)
                EventManager.DropItem(data.dropItemType, transform.position);
            Disappear();
        }
        private IEnumerator Prepare(float delay)
        {
            Enable(false);
            yield return new WaitForSeconds(delay);
            Enable(true);
            notifyAction?.Invoke(EEntityAction.Arrive);
        }
        private void Enable(bool isEnabled)
        {
            col2D.enabled = isEnabled;
            spriteRenderer.enabled = isEnabled;
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ShipBullet") && collision.TryGetComponent<IBullet>(out var bullet))
            {
                if (bullet.gameObject.activeSelf && TakeDamage(bullet.GetDamage()))
                    bullet.Disappear();
            }
        }
        public void Interrupt() => rigi.simulated = false;
    }
}