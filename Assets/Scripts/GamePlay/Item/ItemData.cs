using UnityEngine;

namespace SkyStrike.Game
{
    public class ItemData : GameData<ItemMetaData>
    {
        public static readonly float lifeTime = 20;
        public static readonly Vector3 dropVelocity = new(0, -0.5f, 0);
        public float elapsedTime { get; set; }

        protected override void SetData(ItemMetaData metaData)
            => elapsedTime = 0;
    }
}