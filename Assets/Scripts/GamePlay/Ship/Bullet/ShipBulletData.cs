using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletData : GameData<ShipBulletMetaData, ShipBulletData.ShipBulletEventData>
    {
        public Vector3 velocity { get; set; }
        public float timeLife { get; set; }

        protected override void ChangeData(ShipBulletEventData eventData)
        {
            metaData = eventData.metaData;
            velocity = eventData.velocity;
            timeLife = metaData.timeLife;
        }

        public class ShipBulletEventData : IEventData
        {
            public Vector3 velocity { get; set; }
            public Vector3 position { get; set; }
            public ShipBulletMetaData metaData { get; set; }
        }
    }
}