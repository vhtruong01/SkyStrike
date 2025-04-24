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
            EventManager.onDropItem.AddListener(DropItem);
            EventManager.onDropStar.AddListener(DropStar);
        }
        private void DropItem(EItem type, Vector3 position)
        {
            if (type == EItem.None) return;
            CreateItem(type, position, Random.Range(0, 2 * Mathf.PI));
        }
        private void DropStar(Vector3 position, int amount)
        {
            if (amount == 0) return;
            int star5 = amount / 10;
            int star1 = amount - star5 * 5;
            RandomStar(position, star1, EItem.Star1);
            RandomStar(position, star5, EItem.Star5);
        }
        private void RandomStar(Vector3 position, int amount, EItem type)
        {
            float unitAngle = 2 * Mathf.PI / amount;
            float startAngle = Random.Range(0, Mathf.PI * 2);
            for (int i = 0; i < amount; i++)
                CreateItem(type, position, i * unitAngle + startAngle + Random.Range(0, unitAngle * 2));
        }
        private void CreateItem(EItem type, Vector3 position, float angle)
        {
            if (itemDict.TryGetValue(type, out ItemMetaData itemMetaData))
            {
                Item item = InstantiateItem(position);
                item.data.UpdateDataAndRefresh(itemMetaData);
                item.Appear(Random.Range(0.2f, 1f) * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));
            }
        }
    }
}