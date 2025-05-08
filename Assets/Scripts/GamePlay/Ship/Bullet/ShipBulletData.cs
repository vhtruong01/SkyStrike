using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletData : GameData<BulletData,ShipBulletEventData>
    {
        public float lifetime { get; set; }
        public float angularVelocity { get;set; }
        public Vector3 velocity { get; private set; }

        protected override void ChangeData(ShipBulletEventData eventData)
        {
            metaData = eventData.metaData;
            velocity = eventData.velocity;
            lifetime = metaData.lifetime;
            angularVelocity = 0;
        }
    }
}