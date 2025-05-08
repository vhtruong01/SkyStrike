using UnityEngine;

namespace SkyStrike.Game
{
    public class ItemData : GameData<ItemMetaData, ItemEventData>
    {
        public static readonly float defaultlifetime = 20;
        public static readonly Vector3 dropVelocity = new(0, -0.5f, 0);
        public float lifetime { get; set; }

        protected override void ChangeData(ItemEventData eventData)
        {
            metaData = eventData.metaData;
            lifetime = defaultlifetime;
        }
    }
}