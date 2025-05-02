using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletData : GameData<BulletData, ShipBulletData.ShipBulletEventData>
    {
        public float lifetime { get; set; }
        public EDamageType damageType { get; private set; }
        public Vector3 velocity { get; private set; }

        protected override void ChangeData(ShipBulletEventData eventData)
        {
            metaData = eventData.metaData;
            velocity = eventData.velocity;
            damageType = eventData.damageType;
            lifetime = metaData.lifetime;
        }

        public class ShipBulletEventData : IEventData
        {
            public EDamageType damageType { get; set; }
            public Vector3 velocity { get; set; }
            public Vector3 position { get; set; }
            public BulletData metaData { get; set; }
        }
    }
}