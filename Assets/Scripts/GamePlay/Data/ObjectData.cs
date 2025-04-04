using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class ObjectData : IGameData
        {
            [NonSerialized] public MetaData metaData;
            public int id;
            public int refId;
            public int metaId;
            public float size;
            public int cloneCount;
            public string name;
            public MoveData moveData;
        }
    }
}