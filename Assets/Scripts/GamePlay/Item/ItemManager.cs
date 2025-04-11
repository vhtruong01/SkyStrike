using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ItemManager : PoolManager<Item, ItemData>
        {
            [SerializeField] private List<ItemData> items;
            private Dictionary<EItem, ItemData> itemDict;

            public override void Awake()
            {
                base.Awake();
                itemDict = new();
                foreach (ItemData item in items)
                    itemDict.Add(item.type, item);
                EventManager.onDropItem.AddListener(DropItem);
                EventManager.onDropStar.AddListener(DropStar);
            }
            public void DropItem(EItem type, Vector3 position)
            {
                if (type == EItem.None) return;
                CreateItem(type, position, Random.Range(0, 2 * Mathf.PI));
            }
            public void DropStar(Vector3 position, int amount)
            {
                int star1 = amount % 5;
                int star5 = amount / 5;
                RandomStar(position, star1, EItem.Star1);
                RandomStar(position, star5, EItem.Star5);
            }
            private void RandomStar(Vector3 position, int amount, EItem type)
            {
                float angleUnit = 2 * Mathf.PI / amount;
                float startAngle = Random.Range(0, Mathf.PI * 2);
                for (int i = 0; i < amount; i++)
                    CreateItem(type, position, i * angleUnit + startAngle + Random.Range(0, angleUnit * 2));
            }
            private void CreateItem(EItem type, Vector3 position, float angle)
            {
                if (itemDict.TryGetValue(type, out ItemData itemData))
                {
                    Item item = InstantiateItem(itemData, position);
                    item.Appear(Random.Range(0.5f, 1.5f) * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));
                }
            }
        }
    }
}