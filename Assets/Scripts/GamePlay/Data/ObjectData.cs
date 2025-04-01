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
            public MoveData moveData;
        }
    }
}