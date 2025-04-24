using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipBulletSpawner : MonoBehaviour, ISpawnable
    {
        private class SimpleSpawner
        {
            private float elaspedTime;
            private ShipBulletMetaData metaData;

            public SimpleSpawner(ShipBulletMetaData metaData)
                => this.metaData = metaData;
            public void Update(float deltaTime)
            {
                elaspedTime += deltaTime;
                if (elaspedTime >= metaData.timeCooldown)
                {
                    elaspedTime = 0;
                    EventManager.SpawnShipBullet(metaData);
                }
            }
            public void Upgrade() => metaData.LevelUp();
        }
        [SerializeField] private List<ShipBulletMetaData> bulletDataList;
        private bool isEnabled;
        private Dictionary<EShipBulletType, SimpleSpawner> spawners;

        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Awake()
        {
            spawners = new();
            AddSpawner(EShipBulletType.SingleBullet);
            AddSpawner(EShipBulletType.DoubleBullet);
            AddSpawner(EShipBulletType.TripleBullet);
        }
        public void AddSpawner(EShipBulletType bulletType)
        {
            foreach (ShipBulletMetaData data in bulletDataList)
                if (data.type == bulletType)
                {
                    data.Reset();
                    SimpleSpawner spawner = new(data);
                    spawners.Add(bulletType, spawner);
                    return;
                }
        }
        public void UpgradeSpawner(EShipBulletType bulletType)
        {
            if (spawners.TryGetValue(bulletType, out SimpleSpawner spawner))
                spawner.Upgrade();
            else AddSpawner(bulletType);
        }
        public void Update()
        {
            if (!isEnabled) return;
            float deltaTime = Time.deltaTime;
            foreach (var spawner in spawners.Values)
                spawner.Update(deltaTime);
        }
        public void Spawn() => isEnabled = true;
        public void Stop() => isEnabled = false;
        public void Interrupt() => Stop();
    }
}