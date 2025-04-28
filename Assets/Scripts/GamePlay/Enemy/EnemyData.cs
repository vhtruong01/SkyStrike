namespace SkyStrike.Game
{
    public class EnemyData : GameData<EnemyMetaData, EnemyData.EnemyEventData>
    {
        public EnemyBulletMetaData bulletData { get; set; }
        public MoveData moveData { get; set; }
        public EItem dropItemType { get; set; }
        public int hp { get; set; }
        public bool isDie { get; set; }
        public bool shield { get; set; }
        public bool isImmortal { get; set; }
        public float size { get; set; }
        public bool isMaintain { get; set; }
        public int pointIndex { get; set; }
        public bool isLookingAtPlayer { get; set; }
        public bool isSpawn { get; set; }

        protected override void ChangeData(EnemyEventData eventData)
        {
            metaData = eventData.metaData;
            moveData = eventData.moveData;
            isMaintain = eventData.isMaintain;
            dropItemType = eventData.dropItemType;
            size = eventData.size;
            hp = metaData.maxHp;
            bulletData = null;
            isDie = false;
            shield = false;
            isImmortal = false;
            isSpawn = false;
            isLookingAtPlayer = false;
            pointIndex = -1;
        }

        public class EnemyEventData : IEventData
        {
            public float size { get; set; }
            public bool isMaintain { get; set; }
            public float delay { get; set; }
            public EItem dropItemType { get; set; }
            public UnityEngine.Vector3 position { get; set; }
            public MoveData moveData { get; set; }
            public EnemyMetaData metaData { get; set; }
        }
    }
}