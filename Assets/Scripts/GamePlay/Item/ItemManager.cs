using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ItemManager : PoolManager<Item>
        {
            [SerializeField] private List<ItemData> items;
            private Dictionary<EItem, ItemData> itemDict;
            private List<Item> itemTempList;

            public override void Awake()
            {
                base.Awake();
                itemDict = new();
                itemTempList = new();
                foreach (ItemData item in items)
                    itemDict.Add(item.type, item);
                EventManager.onDropItem.AddListener(DropItem);
                EventManager.onDropStar.AddListener(DropStar);
            }
            public void DropItem(EItem type, Vector3 position)
            {
                if (type == EItem.None) return;
                _ = CreateItem(type, position, Random.Range(0, 2 * Mathf.PI));
            }
            public void DropStar(Vector3 position, int amount)
            {
                if (itemTempList.Count > 0)
                    itemTempList.Clear();
                float angleUnit = 2 * Mathf.PI / amount;
                float startAngle = Random.Range(0, Mathf.PI * 2);
                for (int i = 0; i < amount; i++)
                    _ = CreateItem(EItem.Star1, position, i * angleUnit + startAngle + Random.Range(0, angleUnit * 2));

            }
            private Item CreateItem(EItem type, Vector3 position, float angle)
            {
                Item item = InstantiateItem();
                item.transform.position = position;
                item.SetData(itemDict[type]);
                item.Appear(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));
                return item;
            }
        }
    }
}