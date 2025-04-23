namespace SkyStrike.Game
{
    public class EnemyData : IGame
    {
        public EnemyBulletData bulletData { get; set; }
        public EnemyMetaData metaData { get; set; }
        public MoveData moveData { get; set; }
        public EItem dropItemType { get; set; }
        public int hp { get; set; }
        public bool isDie { get; set; }
        public bool shield { get; set; }
        public bool isImmortal { get; set; }
        public float size { get; set; }
        public bool isMaintain { get; set; }
        public int pointIndex { get; set; }
        public bool isLookingAtPlayer {  get; set; }

        public EnemyData(ObjectData objectData, EnemyMetaData metaData)
        {
            this.metaData = metaData;
            pointIndex = -1;
            size = objectData.size;
            isMaintain = objectData.isMaintain;
            moveData = objectData.moveData;
        }
    }
}