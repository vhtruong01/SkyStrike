using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletData : GameData<ShipBulletMetaData>
    {
        public Vector3 velocity { get; set; }
        public float timeLife { get; set; }

        public void SetExtraData(Vector3 velocity)
            => this.velocity = velocity;
        protected override void SetData(ShipBulletMetaData metaData)
            => timeLife = metaData.timeLife;
    }
}