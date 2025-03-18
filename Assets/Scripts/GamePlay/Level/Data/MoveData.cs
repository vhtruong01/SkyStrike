using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class MoveData : IGameData
        {
            public bool isSyncRotation;
            public Vec2 dir;
            public float rotation;
            public float scale;
            public float delay;
        }
    }
}