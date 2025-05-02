using System;
using UnityEngine;

namespace SkyStrike.Game
{
    [Serializable]
    public class EnemyBulletMetaData : IMetaData, IGame
    {
        public int id;
        public string name;
        public float size;
        public float velocity;//speed
        public float timeCooldown;
        public float spinSpeed;
        public float lifetime;
        public float unitAngle;
        public float startAngle;
        public Vec2 spacing;
        public Vec2 position;
        public bool isCircle;
        public bool isStartAwake;
        public bool isLookingAtPlayer;
        public int amount;
        [NonSerialized] public Color color;
    }
}