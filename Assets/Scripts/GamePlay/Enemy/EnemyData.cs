using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyData : GameData<EnemyMetaData>
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

        public void SetExtraData(ObjectData objectData,EItem itemType)
        {
            moveData = objectData.moveData;
            size = objectData.size;
            isMaintain = objectData.isMaintain;
            dropItemType = itemType;
        }
        protected override void SetData(EnemyMetaData data)
        {
            bulletData = null;
            metaData = data;
            hp = metaData.maxHp;
            isDie = false;
            shield = false;
            isImmortal = false;
            pointIndex = -1;
            isLookingAtPlayer = false;
        }
    }
}