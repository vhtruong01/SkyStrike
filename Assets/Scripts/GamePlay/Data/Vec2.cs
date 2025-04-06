using System;
using System.Runtime.CompilerServices;
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
            public readonly Vector2 ToVector2() => new(x, y);
            public readonly Vector3 ToVector3() => new(x, y, 0);
            public override string ToString() => $"[{x},{y}]";

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec2 operator +(Vec2 a, Vec2 b) => new(a.x + b.x, a.y + b.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec2 operator -(Vec2 a, Vec2 b) => new(a.x - b.x, a.y - b.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec2 operator *(float f, Vec2 a) => new(a.x * f, a.y * f);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec2 operator *(Vec2 a, float f) => new(a.x * f, a.y * f);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec2 operator /(Vec2 a, float f) => new(a.x / f, a.y / f);
        }
    }
}