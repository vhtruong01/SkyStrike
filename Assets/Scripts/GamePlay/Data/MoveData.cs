using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class MoveData : IGameData
        {
            [Serializable]
            public class Point : ICloneable
            {
                public Vec2 prevPos;
                public Vec2 midPos;
                public Vec2 nextPos;
                public bool isStraightLine;
                public bool isImmortal;
                public bool isLookAtPlayer;
                public bool isFixedRotation;
                public float rotation;
                public float scale;
                public float accleration;
                public float standingTime;
                public float travelTime;

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
}