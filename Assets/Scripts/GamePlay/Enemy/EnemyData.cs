namespace SkyStrike.Game
{
    public class EnemyData : IGame
    {
        public EnemyBulletData bulletData;
        public EnemyMetaData metaData;
        public MoveData moveData;
        public EItem dropItemType;
        public int hp;
        public bool isDie;
        public bool shield;
        public bool isImmortal;
        public float size;
        public bool isMaintain;
        public bool isAutoMove;
        public int pointIndex;

        public EnemyData(ObjectData objectData,EnemyMetaData metaData)
        {
            this.metaData = metaData;
            size = objectData.size;
            isMaintain = objectData.isMaintain;
            if (!isAutoMove)
                moveData = objectData.moveData;
        }
    }
}