using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public sealed class Ship : Commander, IShipComponent, IEntity, ICollector
    {
        public static Vector3 pos { get; private set; }
        [field: SerializeField] public ShipData shipData { get; set; }
        private Rigidbody2D rigi;

        public IEnumerator Start()
        {

            // test
            yield return StartCoroutine(movement.Travel(2));
        }
        public override void Init()
            => rigi = GetComponent<Rigidbody2D>();
        private void Update()
            => pos = entity.transform.position;
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
            => rigi.simulated = false;
        public void Collect(EItem item)
        {
            if (item != EItem.Star1 && item != EItem.Star5)
                animator.GetAnimation(EAnimationType.Highlight).Restart();
            shipData.CollectItem(item);
        }
        public bool TakeDamage(IDamager damager)
        {
            //
            if (shipData.invincibility) return false;
            if (!shipData.shield)
            {
                shipData.health -= damager.GetDamage();
                if (shipData.health <= 0)
                    Die();
                else
                {
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