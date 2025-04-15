using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class EnemyBulletData : IGame
        {
            public int id;
            public string name;
            public float size;
            public float velocity;
            public float timeCooldown;
            public float spinSpeed;
            public float lifeTime;
            public float angleUnit;
            public float startAngle;
            public Vec2 spacing;
            public Vec2 position;
            public bool isCircle;
            public bool isStartAwake;
            public bool isLookAtPlayer;
            public int amount;
        }
    }
}