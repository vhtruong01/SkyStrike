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
                public bool isStraight;
            }
            public bool isLoop;
            public bool isSyncRotation;
            public Vec2 dir;
            public float rotation;
            public float scale;
            public float delay;
            public float accleration;
            public float radius;
            public Point[] points;
        }
    }
}