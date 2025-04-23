using System;

namespace SkyStrike.Game
{
    [Serializable]
    public class MoveData : IGame
    {
        [Serializable]
        public class Point : ICloneable, IGame
        {
            [NonSerialized] public EnemyBulletData[] bulletDataList;
            public Vec2 prevPos;
            public Vec2 midPos;
            public Vec2 nextPos;
            public bool isStraightLine;
            public bool isImmortal;
            public bool isLookingAtPlayer;
            public bool isIgnoreVelocity;
            public bool shield;
            public float rotation;
            public float scale;
            public float standingTime;
            public float travelTime;
            public int[] bulletIdArr;

            public object Clone() => MemberwiseClone();
        }
        public Point[] points;
        public float delay;
        public float velocity;

        public void Translate(Vec2 pos)
        {
            Vec2 dir = pos - points[0].midPos;
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = points[i].Clone() as Point;
                points[i].prevPos += dir;
                points[i].midPos += dir;
                points[i].nextPos += dir;
            }
        }
        public MoveData Clone()
        {
            MoveData newData = new()
            {
                delay = delay,
                velocity = velocity,
                points = new Point[points.Length]
            };
            for (int i = 0; i < newData.points.Length; i++)
                newData.points[i] = points[i].Clone() as Point;
            return newData;
        }
    }
}