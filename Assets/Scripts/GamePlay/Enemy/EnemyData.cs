using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class EnemyData : ObjectEntityData<EnemyMetaData>
    {
        public EnemyBulletMetaData bulletData { get; set; }
        public EItem dropItemType { get; private set; }
        private int _hp;
        private bool _shield;
        public int hp
        {
            get => _hp;
            set
            {
                _hp = value;
                onHealthChanged?.Invoke(_hp);
            }
        }
        public bool shield
        {
            get => _shield;
            set
            {
                _shield = value;
                onShieldActive?.Invoke(_shield);
            }
        }
        public bool isSpawn { get; set; }
        public bool isImmortal { get; set; }
        public bool isLookingAtPlayer { get; set; }
        public UnityAction<int> onHealthChanged { get; set; }
        public UnityAction<bool> onShieldActive { get; set; }

        protected override void ChangeData(ObjectEventData<EnemyMetaData> eventData)
        {
            base.ChangeData(eventData);
            dropItemType = eventData.dropItemType;
            hp = metaData.maxHp;
            bulletData = null;
            shield = false;
            isImmortal = false;
            isSpawn = false;
            isLookingAtPlayer = false;
        }
    }
}