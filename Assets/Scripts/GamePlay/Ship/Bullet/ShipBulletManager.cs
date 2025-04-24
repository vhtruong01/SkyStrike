using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletManager : PoolManager<ShipBullet, ShipBulletData>
    {
        public override void Awake()
        {
            base.Awake();
            EventManager.onSpawnShipBullet.AddListener(SpawnBullet);
        }
        private void SpawnBullet(ShipBulletMetaData metaData)
        {
            Vector3 pos = transform.position;
            switch (metaData.type)
            {
                case EShipBulletType.SingleBullet:
                    SpawnBullet(metaData, pos, new(0, metaData.speed, 0));
                    break;
                case EShipBulletType.DoubleBullet:
                    SpawnBullet(metaData, pos + new Vector3(-0.25f, 0, 0), new(0, metaData.speed, 0));
                    SpawnBullet(metaData, pos + new Vector3(0.25f, 0, 0), new(0, metaData.speed, 0)); 
                    break;
                case EShipBulletType.TripleBullet:
                    SpawnBullet(metaData, pos, new(-metaData.speed * Mathf.Sin(Mathf.PI / 12), metaData.speed, 0));
                    SpawnBullet(metaData, pos, new(0, metaData.speed, 0));
                    SpawnBullet(metaData, pos, new(metaData.speed * Mathf.Sin(Mathf.PI / 12), metaData.speed, 0));
                    break;
                case EShipBulletType.LaserBullet:
                    break;
                case EShipBulletType.RocketBullet:
                    break;
            }
        }
        private void SpawnBullet(ShipBulletMetaData metaData, Vector3 pos, Vector3 velocity)
        {
            var bullet = InstantiateItem(pos);
            bullet.data.SetExtraData(velocity);
            bullet.data.UpdateDataAndRefresh(metaData);
        }
    }
}