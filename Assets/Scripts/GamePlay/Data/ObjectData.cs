using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class ObjectData : IGame
        {
            [NonSerialized] public EnemyMetaData metaData;
            public int id;
            public int refId;
            public int metaId;
            public float size;
            public int cloneCount;
            public float spawnInterval;
            public string name;
            public bool isMaintain;
            public MoveData moveData;
            public EItem dropItemType;

            public Vec2 pos => moveData.points[0].midPos;
        }
    }
}