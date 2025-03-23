using System;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public struct Vec2
        {
            public float x;
            public float y;

            public Vec2(Vector2 v2)
            {
                x = v2.x;
                y = v2.y;
            }
            public Vec2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
            public readonly Vector2 Get() => new (x, y);
        }
    }
}