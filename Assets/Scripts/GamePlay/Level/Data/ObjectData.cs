using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class ObjectData : IGameData
        {
            public int id;
            public int refId;
            public int metaId;
            public float delay;
            public string name;
            public float rotation;
            public Vec2 scale;
            public Vec2 velocity;
            public Vec2 position;
            public PhaseData phase;
        }
    }
}