using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class MoveData : IGameData
        {
            [Serializable]
            public class Point
            {
                public Vec2 prevPos;
                public Vec2 midPos;
                public Vec2 nextPos;
                public bool isStraightLine;
                public bool isImmortal;
                public bool isLookAtPlayer;
                public bool isFixedRotation;
                public float rotation;
                public float accleration;
                public float scale;
                public float standingTime;
                public float travelTime;
            }
            public Point[] points;
        }
    }
}