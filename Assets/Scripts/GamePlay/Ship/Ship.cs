using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class Ship : MonoBehaviour, IShipComponent, IEntity, IRefreshable, ICollector
    {
        public static Vector3 pos { get; private set; }
        private Rigidbody2D rigi;
        public IEntity entity { get; set; }
        public ShipData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Init()
            => rigi = GetComponent<Rigidbody2D>();
        private void Update()
            => pos = entity.transform.position;
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
        public void DropItemAndDisappear()
        {
            //lose
            //notifyAction.Invoke(EEntityAction.Lose);
        }
        public void Disappear()
        {
            //win
            //notifyAction.Invoke(EEntityAction.Win);
        }
        public void Die()
        {

            //=> notifyAction?.Invoke(EEntityAction.Die);
        }
        public void Interrupt() => rigi.simulated = false;
        public void Collect(EItem item)
            => data.Collect(item);
        public void Refresh()
        {
            // start game
            rigi.simulated = true;
        }
        public bool TakeDamage(int damage)
        {
            if (data.invincibility) return false;
            if (!data.shield)
            {
                data.hp -= damage;
                if (data.hp <= 0)
                    Die();
                else notifyAction.Invoke(EEntityAction.TakeDamage);
            }
            return true;
        }
    }
}