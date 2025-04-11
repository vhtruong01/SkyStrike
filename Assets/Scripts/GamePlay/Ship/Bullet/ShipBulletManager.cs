using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ShipBulletManager : PoolManager<ShipBullet, ShipBulletData>
        {
            [SerializeField] private List<ShipBulletMetaData> bulletDataList;
            private Dictionary<EShipBulletType, ShipBulletSpawner> spawners;

            public override void Awake()
            {
                base.Awake();
                spawners = new();
                AddSpawner(EShipBulletType.NormalBullet);
                AddSpawner(EShipBulletType.DoubleBullet);
                AddSpawner(EShipBulletType.TripleBullet);
                EnableFire(true);
            }
            public void AddSpawner(EShipBulletType bulletType)
            {
                foreach (ShipBulletMetaData data in bulletDataList)
                    if (data.type == bulletType)
                    {
                        ShipBulletSpawner spawner = gameObject.AddComponent<ShipBulletSpawner>();
                        ShipBulletData bulletData = new(bulletDataList[(int)bulletType]);
                        spawner.Init(bulletData, InstantiateItem);
                        spawners.Add(bulletType, spawner);
                        return;
                    }
            }
            public void UpgradeSpawner(EShipBulletType bulletType)
            {
                if (spawners.TryGetValue(bulletType, out ShipBulletSpawner spawner))
                    spawner.Upgrade();
                else AddSpawner(bulletType);
            }
            public void EnableFire(bool isEnable)
            {
                foreach (var spawner in spawners.Values)
                    spawner.isEnable = isEnable;
            }
        }
    }
}