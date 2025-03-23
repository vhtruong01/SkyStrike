using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class MoveData : IGameData
        {
            public bool isLoop;
            public bool isSyncRotation;
            public Vec2 dir;
            public float rotation;
            public float scale;
            public float delay;
            public float accleration;
            public float radius;
            public override string ToString()
            {
                return dir.ToString();
            }
        }
    }
}