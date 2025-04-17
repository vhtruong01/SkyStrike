namespace SkyStrike
{
    namespace Game
    {
        public class ShipBulletData : IGame
        {
            public int lv { get; protected set; }
            public int dmg { get; protected set; }
            public float speed { get; protected set; }
            public float timeCooldown { get; protected set; }
            public ShipBulletMetaData metaData { get; protected set; }

            public ShipBulletData(ShipBulletMetaData metaData)
            {
                this.metaData = metaData;
                lv = 1;
                dmg = metaData.dmg;
                speed = metaData.speed;
                timeCooldown = metaData.timeCooldown;
            }
            public virtual void LevelUp()
            {
                if (lv < metaData.maxLevel)
                {
                    lv += 1;
                }
            }
        }
    }
}