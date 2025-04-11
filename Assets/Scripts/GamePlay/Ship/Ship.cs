using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Ship : MonoBehaviour
        {
            [SerializeField] private ShipData shipData;

            public void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("Item"))
                {
                    Item item = collision.GetComponent<Item>();
                    CollectItem(item);
                    item.Release();
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
                        {

                            shipData.hp++;
                        }
                        break;
                    case EItem.Comet:
                        break;
                    case EItem.Shield:
                        break;
                }
            }
        }
    }
}