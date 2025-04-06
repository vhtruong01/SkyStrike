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

            public override void Awake()
            {
                base.Awake();
                itemDict = new();
                foreach (ItemData item in items)
                    itemDict.Add(item.type, item);
                //EventManager.onCreateItem.AddListener(CreateItem);
                //EventManager.onRemoveItem.AddListener(RemoveItem);
            }
            public Item CreateItem(EItem itemType)
            {
                if (itemType == EItem.None) return null;
                Item item = CreateItem();
                item.SetData(itemDict[itemType]);
                return item;
            }
        }
    }
}