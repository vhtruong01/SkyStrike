using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "Bullet", menuName = "Data/Bullet")]
        public class ShipBulletData : ScriptableObject
        {
            public int dmg;
            public int speed;
            public float timeLife;
            public float timeCooldown;
            public Sprite sprite;
            public EShipBulletType type;
        }
    }
}