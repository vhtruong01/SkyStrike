using System;

namespace SkyStrike.Game
{

    [Serializable]
    public class ObjectData : IGame
    {
        public int id;
        public int refId;
        public int metaId;
        public int cloneCount;
        public float spawnInterval;
        public string name;
        public float size;
        public bool isMaintain;
        public MoveData moveData;
        public EItem dropItemType;

        public Vec2 pos => moveData.points[0].midPos;
        public void CopyMoveData(MoveData newMoveData)
        {
            newMoveData.Translate(pos);
            moveData.points = newMoveData.points;
        }
    }
}