using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletData : IGame
    {
        public int lv { get; protected set; }
        public int dmg { get; protected set; }
        public float speed { get; protected set; }
        public float timeCooldown { get; protected set; }
        public float scale { get; protected set; }
        public ShipBulletMetaData metaData { get; protected set; }

        public ShipBulletData(ShipBulletMetaData metaData)
        {
            this.metaData = metaData;
            lv = 0;
            scale = metaData.scale;
            dmg = metaData.dmg;
            speed = metaData.speed;
            timeCooldown = metaData.timeCooldown;
        }
        public virtual void LevelUp()
        {
            if (lv < metaData.maxLevel)
            {
                if (lv < metaData.dmgLvUp.Count)
                    dmg = metaData.dmgLvUp[lv];
                if (lv < metaData.cooldownLvUp.Count)
                    timeCooldown = metaData.cooldownLvUp[lv];
                if (lv < metaData.speedLvUp.Count)
                    speed = metaData.speedLvUp[lv];
                if (lv < metaData.scaleLvUp.Count)
                    scale = metaData.scaleLvUp[lv];
                lv += 1;
            }
        }
    }
}