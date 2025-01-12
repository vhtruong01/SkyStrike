using UnityEngine;

namespace SkyStrike
{
    namespace Ship
    {
        [CreateAssetMenu(fileName ="Bullet",menuName ="Data/Bullet")]
        public class BulletData : ScriptableObject
        {
            public float timeLife;
            public string type;
            public float timeCountdown;
            public int dmg;
            public int speed;
        }
    }
}