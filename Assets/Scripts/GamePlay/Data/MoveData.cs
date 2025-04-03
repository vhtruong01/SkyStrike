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

            public MoveData Clone(Vec2 pos)
            {
                MoveData newData = new();
                newData.points = new Point[points.Length];
                Vec2 dir = pos - points[0].midPos;
                for (int i = 0; i < newData.points.Length; i++)
                {
                    newData.points[i] = points[i].Clone() as Point;
                    newData.points[i].prevPos += dir;
                    newData.points[i].midPos += dir;
                    newData.points[i].nextPos += dir;
                }
                return newData;
            }
        }
    }
}