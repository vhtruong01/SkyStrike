using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(ShipData))]
    public class Ship : MonoBehaviour, IEntity, IRefreshable
    {
        public static Vector3 pos { get; private set; }
        [SerializeField] private SpriteRenderer planet;
        [SerializeField] private SpriteRenderer shield;
        public ShipData data { get; private set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Awake()
        {
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
            planet.transform.position = Vector3.zero;
            planet.transform.localScale = new(20, 20, 20);
            planet.gameObject.SetActive(true);
            data = GetComponent<ShipData>();
        }
        public void Update() => pos = transform.position;
        public IEnumerator PrepareFlying()
        {
            float appearTime = 2.5f;
            transform.DOScale(1.25f, appearTime);
            planet.transform.DOScale(0, appearTime);
            notifyAction.Invoke(EEntityAction.StopAttack);
            yield return new WaitForSeconds(appearTime);
            planet.gameObject.SetActive(false);
        }
        public IEnumerator StartFlying()
        {
            float appearTime = 1f;
            shield.DOFade(0, appearTime);
            shield.transform.DOScale(1.5f, appearTime);
            yield return new WaitForSeconds(1);
            //command.SetAttackEnabled(true);

            shield.gameObject.SetActive(false);
            notifyAction.Invoke(EEntityAction.Attack);
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                var item = collision.GetComponent<IItem>();
                if (item.gameObject.activeSelf)
                {
                    CollectItem(item.GetItemType());
                    item.Disappear();
                }
                return;
            }
            if (collision.CompareTag("EnemyBullet"))
            {
                //
            }
        }
        private void CollectItem(EItem type)
        {
            switch (type)
            {
                case EItem.Star1:
                    data.star++;
                    break;
                case EItem.Star5:
                    data.star += 5;
                    break;
                case EItem.Health:
                    data.hp++;
                    break;
                //case EItem.SingleBullet:
                //    command.UpgradeBullet(EShipBulletType.SingleBullet);
                //    break;
                //case EItem.DoubleBullet:
                //    command.UpgradeBullet(EShipBulletType.DoubleBullet);
                //    break;
                //case EItem.TripleBullet:
                //    command.UpgradeBullet(EShipBulletType.TripleBullet);
                //    break;
                //case EItem.LaserBullet:
                //    command.UpgradeBullet(EShipBulletType.LaserBullet);
                //    break;
                case EItem.Comet:
                    break;
                case EItem.Shield:
                    break;
            }
        }
        public void DropItemAndDisappear()
        {

        }
        public void Disappear()
        {
        }
        public void Die()
        {
        }
        public void Interrupt()
        {
        }
        public void Refresh()
        {
        }
    }
}