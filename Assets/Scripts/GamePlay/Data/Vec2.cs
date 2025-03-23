using UnityEngine;
using System;
using System.Runtime.CompilerServices;

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
            public static Vector3 operator +(Vec2 a, Vector3 b) => new(a.x + b.x, a.y + b.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator +(Vector3 b, Vec2 a) => new(b.x + a.x, b.y + a.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator -(Vec2 a, Vector3 b) => new(a.x - b.x, a.y - b.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator -(Vector3 b, Vec2 a) => new(b.x - a.x, b.y - a.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator *(Vec2 a, Vector3 b) => new(a.x * b.x, a.y * b.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator *(Vector3 b, Vec2 a) => new(b.x * a.x, b.y * a.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator /(Vec2 a, Vector3 b) => new(a.x / b.x, a.y / b.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator /(Vector3 b, Vec2 a) => new(b.x / a.x, b.y / a.y, b.z);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator +(Vec2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator +(Vector2 b, Vec2 a) => new(b.x + a.x, b.y + a.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator -(Vec2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator -(Vector2 b, Vec2 a) => new(b.x - a.x, b.y - a.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator *(Vec2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator *(Vector2 b, Vec2 a) => new(b.x * a.x, b.y * a.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator /(Vec2 a, Vector2 b) => new(a.x / b.x, a.y / b.y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator /(Vector2 b, Vec2 a) => new(b.x / a.x, b.y / a.y);
        }
    }
}