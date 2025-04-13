using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class BulletData : IGame
        {
            public int id;
            public string name;
            public float size;
            public float velocity;
            public float timeCooldown;
            public float spinSpeed;
            public float length;
            public float angleUnit;
            public Vec2 spacing;
            public Vec2 position;
            public bool isCircle;
            public bool isStartAwake;
            public int amount;
        }
    }
}