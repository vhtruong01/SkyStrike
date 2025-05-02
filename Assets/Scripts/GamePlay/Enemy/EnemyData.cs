namespace SkyStrike.Game
{
    public class EnemyData : GameData<EnemyMetaData, EnemyData.EnemyEventData>
    {
        public EnemyBulletMetaData bulletData { get; set; }
        public MoveData moveData { get; private set; }
        public EItem dropItemType { get; private set; }
        public int hp { get; set; }
        public bool shield { get; set; }
        public int pointIndex { get; set; }
        public bool isSpawn { get; set; }
        public bool canMove { get; set; }
        public bool isImmortal { get; set; }
        public float size { get; private set; }
        public bool isLookingAtPlayer { get; set; }
        public bool isMaintain { get; private set; }

        protected override void ChangeData(EnemyEventData eventData)
        {
            metaData = eventData.metaData;
            moveData = eventData.moveData;
            isMaintain = eventData.isMaintain;
            dropItemType = eventData.dropItemType;
            size = eventData.size;
            hp = metaData.maxHp;
            bulletData = null;
            shield = false;
            isImmortal = false;
            isSpawn = false;
            isLookingAtPlayer = false;
            pointIndex = 0;
            canMove = true;
        }

        public class EnemyEventData : IEventData
        {
            public int metaId { get; set; }
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