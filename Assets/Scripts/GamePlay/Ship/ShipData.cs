using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipData : GameData<ShipMetaData>
    {
        [SerializeField] private ShipMetaData shipMetaData;
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
        public int maxHp { get; set; }
        public float speed { get; set; }
        public UnityEvent<int> onCollectStar { get; private set; }
        public UnityEvent<int> onCollectHealth { get; private set; }

        public override void Awake()
        {
            base.Awake();
            onCollectStar = new();
            onCollectHealth = new();
            shipMetaData.Reset();
        }
        public void Start()
            => UpdateDataAndRefresh(shipMetaData);
        protected override void SetData(ShipMetaData metaData)
        {
            _hp = metaData.hp;
            _star = metaData.star;
            maxHp = metaData.maxHp;
            speed = metaData.speed;
        }
    }
}