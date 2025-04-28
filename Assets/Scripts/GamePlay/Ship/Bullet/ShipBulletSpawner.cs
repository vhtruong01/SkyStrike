using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipBulletSpawner : MonoBehaviour, IShipComponent, ISpawnable
    {
        [SerializeField] private List<ShipBulletMetaData> bulletDataList;
        private Dictionary<EShipBulletType, SimpleSpawner> spawners;
        public IEntity entity { get; set; }
        public ShipData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Init()
        {
            spawners = new();
            data.onUpgradeSpawner.AddListener(UpgradeSpawner);
            AddSpawner(EShipBulletType.SingleBullet);
        }
        private void AddSpawner(EShipBulletType bulletType)
        {
            foreach (ShipBulletMetaData data in bulletDataList)
                if (data.type == bulletType)
                {
                    data.Reset();
                    SimpleSpawner spawner = new(data, entity);
                    spawners.Add(bulletType, spawner);
                    return;
                }
        }
        private void UpgradeSpawner(EShipBulletType bulletType)
        {
            if (spawners.TryGetValue(bulletType, out SimpleSpawner spawner))
                spawner.Upgrade();
            else AddSpawner(bulletType);
        }
        private void Update()
        {
            if (!data.isSpawn) return;
            float deltaTime = Time.deltaTime;
            foreach (var spawner in spawners.Values)
                spawner.Update(deltaTime);
        }
        public void Spawn() => data.isSpawn = true;
        public void Stop() => data.isSpawn = false;
        public void Interrupt() => Stop();

        private class SimpleSpawner
        {
            private readonly ShipBulletData.ShipBulletEventData bulletEventData = new();
            private readonly IEntity entity;
            private float elaspedTime;

            public SimpleSpawner(ShipBulletMetaData metaData, IEntity entity)
            {
                this.entity = entity;
                bulletEventData.metaData = metaData;
            }
            public void Update(float deltaTime)
            {
                elaspedTime += deltaTime;
                if (elaspedTime >= bulletEventData.metaData.timeCooldown)
                {
                    elaspedTime = 0;
                    bulletEventData.position = entity.position;
                    EventManager.Active(bulletEventData);
                }
            }
            public void Upgrade() => bulletEventData.metaData.LevelUp();
        }
    }
}