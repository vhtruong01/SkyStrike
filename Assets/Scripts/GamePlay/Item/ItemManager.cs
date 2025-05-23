using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class ItemManager : PoolManager<Item, ItemData>
    {
        [SerializeField] private List<ItemMetaData> items;
        private Dictionary<EItem, ItemMetaData> itemDict;

        public override void Awake()
        {
            base.Awake();
            itemDict = new();
            foreach (ItemMetaData item in items)
                itemDict.Add(item.type, item);
        }
        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<ItemEventData>(DropItem);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<ItemEventData>(DropItem);
        }
        private void DropItem(ItemEventData eventData)
        {
            if (eventData.itemType == EItem.None || eventData.amount == 0) return;
            float unitAngle = 2 * Mathf.PI / eventData.amount;
            float startAngle = Random.Range(0, Mathf.PI * 2);
            for (int i = 0; i < eventData.amount; i++)
                if (itemDict.TryGetValue(eventData.itemType, out var metaData))
                {
                    eventData.metaData = metaData;
                    float angle = i * unitAngle + startAngle + Random.Range(0, unitAngle * 2);
                    Item item = InstantiateItem(eventData.position);
                    item.data.SetData(eventData);
                    item.Appear(Random.Range(0.5f, 0.7f + eventData.amount * 0.05f) * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));
                }
        }
    }
}