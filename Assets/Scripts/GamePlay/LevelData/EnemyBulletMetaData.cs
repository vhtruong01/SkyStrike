using System;
using UnityEngine;

namespace SkyStrike.Game
{
    [Serializable]
    public class EnemyBulletMetaData : IMetaData, IGame
    {
        [Serializable]
        public class BulletStateData : IGame
        {
            public float scale;
            public float coef;
            public float duration;
            public float rotation;
            public bool isAuto;
        }

        public int id;
        public string name;
        public float size;
        public float speed;
        public float timeCooldown;
        public float spinSpeed;
        public float lifetime;
        public float unitAngle;
        public float startAngle;
        public Vec2 spacing;
        public Vec2 position;
        public bool isCircle;
        public bool isStartAwake;
        public bool isUseState;
        public int amount;
        public BulletStateData[] states;
        [NonSerialized] public Color color;
    }
}