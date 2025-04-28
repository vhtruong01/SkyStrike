using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum EShipBulletType
    {
        SingleBullet,
        DoubleBullet,
        TripleBullet,
        LaserBullet,
        MissileBullet,
        MagicBullet,
    }
    [CreateAssetMenu(fileName = "Bullet", menuName = "Data/Bullet")]
    public class ShipBulletMetaData : ScriptableObject, IMetaData, IGame
    {
        public int lv { get; private set; }
        public int dmg { get; private set; }
        public float speed { get; private set; }
        public float scale { get; private set; }
        public float timeCooldown { get; private set; }
        [field: SerializeField] public int maxLevel { get; private set; }
        [field: SerializeField] public float timeLife { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Material material { get; private set; }
        [field: SerializeField] public EShipBulletType type { get; private set; }
        [field: SerializeField] public List<int> dmgLvUp { get; private set; }
        [field: SerializeField] public List<float> speedLvUp { get; private set; }
        [field: SerializeField] public List<float> cooldownLvUp { get; private set; }
        [field: SerializeField] public List<float> scaleLvUp { get; private set; }
        public void LevelUp()
        {
            lv++;
            if (lv < maxLevel)
            {
                if (lv < dmgLvUp.Count)
                    dmg = dmgLvUp[lv];
                if (lv < cooldownLvUp.Count)
                    timeCooldown = cooldownLvUp[lv];
                if (lv < speedLvUp.Count)
                    speed = speedLvUp[lv];
                if (lv < scaleLvUp.Count)
                    scale = scaleLvUp[lv];
            }
        }
        public void Reset()
        {
            lv = -1;
            speed = 5;
            dmg = 100;
            scale = 1;
            timeCooldown = 1;
            LevelUp();
        }
    }
}