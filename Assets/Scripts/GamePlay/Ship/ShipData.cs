using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipData : GameData<ShipMetaData, ShipData>, IEventData
    {
        private int _hp;
        private int _star;
        public int hp
        {
            get => _hp;
            set
            {
                if (value < maxHp)
                {
                    _hp = value;
                    onCollectHealth.Invoke(hp);
                }
            }
        }
        public int star
        {
            get => _star;
            set
            {
                _star = value;
                onCollectStar.Invoke(_star);
            }
        }
        public int lv { get; set; }
        public int exp { get; set; }
        public int weaponPoint { get; set; }
        public float magnetRadius { get; set; }
        public int maxHp { get; set; }
        public bool canMove { get; set; }
        public bool shield { get; set; }
        public bool invincibility { get; set; }
        public float speed { get; set; }
        public bool isSpawn { get; set; }
        public UnityEvent<int> onCollectStar { get; private set; } = new();
        public UnityEvent<int> onCollectHealth { get; private set; } = new();
        public UnityEvent<EShipBulletType> onUpgradeSpawner { get; private set; } = new();

        public override void Awake()
        {
            base.Awake();
            SetData(this);
        }
        protected override void ChangeData(ShipData eventData)
        {
            metaData = eventData.metaData;
            metaData.Reset();
            _star = 0;
            _hp = metaData.hp;
            speed = metaData.speed;
            maxHp = metaData.maxHp;
            magnetRadius = metaData.magnetRadius;
        }
        public void Collect(EItem type)
        {
            switch (type)
            {
                case EItem.Star1:
                    star++;
                    break;
                case EItem.Star5:
                    star += 5;
                    break;
                case EItem.Health:
                    hp++;
                    break;
                case EItem.WeaponPoint:
                    weaponPoint++;
                    break;
                case EItem.Comet:
                    //cometItem++;
                    break;
                case EItem.Shield:
                    //shieldItem++;
                    break;
                    //
            }
        }
    }
}