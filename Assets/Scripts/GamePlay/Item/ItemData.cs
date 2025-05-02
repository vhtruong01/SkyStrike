using UnityEngine;

namespace SkyStrike.Game
{
    public class ItemData : GameData<ItemMetaData, ItemData.ItemEventData>
    {
        public static readonly float defaultlifetime = 10;
        public static readonly Vector3 dropVelocity = new(0, -0.5f, 0);
        public float lifetime { get; set; }

        protected override void ChangeData(ItemEventData eventData)
        {
            metaData = eventData.metaData;
            lifetime = defaultlifetime;
        }

        public class ItemEventData : IEventData
        {
            public int amount { get; set; }
            public EItem itemType { get; set; }
            public Vector3 position { get; set; }
            public ItemMetaData metaData { get; set; }
        }
    }
}