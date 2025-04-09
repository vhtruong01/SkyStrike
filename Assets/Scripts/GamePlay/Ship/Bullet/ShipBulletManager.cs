using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ShipBulletManager : PoolManager<ShipBullet>
        {
            [SerializeField] private List<ShipBulletData> bulletDataList;
            [SerializeField] protected ShipBullet bulletPrefab;
            [SerializeField] private Transform bulletContainer;
            private Dictionary<EShipBulletType, ShipBulletSpawner> spawners;

            public override void Awake()
            {
                base.Awake();
                spawners = new();
                AddSpawner(EShipBulletType.Laser);
            }
            protected override ShipBullet Create()
            {
                ShipBullet bullet = base.Create();
                bullet.transform.SetParent(bulletContainer, false);
                return bullet;
            }
            public void AddSpawner(EShipBulletType bulletType)
            {
                ShipBulletSpawner spawner = gameObject.AddComponent<ShipBulletSpawner>();
                spawner.bulletPool = pool;
                spawner.bulletData = bulletDataList[(int)bulletType];
                spawners.Add(bulletType, spawner);
            }
            public void UpgradeSpawner(EShipBulletType bulletType)
            {
                if (spawners.TryGetValue(bulletType, out ShipBulletSpawner spawner))
                    spawner.Upgrade();
                else AddSpawner(bulletType);
            }
        }
    }
}