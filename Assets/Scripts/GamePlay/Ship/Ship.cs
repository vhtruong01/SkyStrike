using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipMetaData shipMetaData;
        [SerializeField] private SpriteRenderer planet;
        [SerializeField] private SpriteRenderer shield;
        private ShipCommand command;
        public ShipData shipData { get; private set; }

        public void Awake()
        {
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
            planet.transform.position = Vector3.zero;
            planet.transform.localScale = new(20, 20, 20);
            planet.gameObject.SetActive(true);
            command = GetComponent<ShipCommand>();
            command.SetAttackEnabled(false);
            shipData = new(shipMetaData);
        }
        public IEnumerator PrepareFlying()
        {
            float appearTime = 2.5f;
            transform.DOScale(1.25f, appearTime);
            planet.transform.DOScale(0, appearTime);
            yield return new WaitForSeconds(appearTime);
            planet.gameObject.SetActive(false);
        }
        public IEnumerator StartFlying()
        {
            float appearTime = 1f;
            shield.DOFade(0, appearTime);
            shield.transform.DOScale(1.5f, appearTime);
            yield return new WaitForSeconds(1);
            command.SetAttackEnabled(true);
            shield.gameObject.SetActive(false);
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                Item item = collision.GetComponent<Item>();
                if (item.gameObject.activeSelf)
                {
                    CollectItem(item);
                    item.Disappear();
                }
                return;
            }
            if (collision.CompareTag("EnemyBullet"))
            {
                //
            }
        }
        private void CollectItem(Item item)
        {
            switch (item.GetItemType())
            {
                case EItem.Star1:
                    shipData.star++;
                    break;
                case EItem.Star5:
                    shipData.star += 5;
                    break;
                case EItem.Health:
                    if (shipData.hp < shipData.maxHp)
                        shipData.hp++;
                    break;
                case EItem.SingleBullet:
                    command.UpgradeBullet(EShipBulletType.SingleBullet);
                    break;
                case EItem.DoubleBullet:
                    command.UpgradeBullet(EShipBulletType.DoubleBullet);
                    break;
                case EItem.TripleBullet:
                    command.UpgradeBullet(EShipBulletType.TripleBullet);
                    break;
                case EItem.LaserBullet:
                    command.UpgradeBullet(EShipBulletType.LaserBullet);
                    break;
                case EItem.Comet:
                    break;
                case EItem.Shield:
                    break;
            }
        }
    }
}