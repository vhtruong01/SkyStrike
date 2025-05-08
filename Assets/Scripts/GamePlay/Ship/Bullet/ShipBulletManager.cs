using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletManager : PoolManager<ShipBullet, ShipBulletData>
    {
        private readonly ShipBulletEventData bulletEventData = new();

        private void OnEnable()
            => EventManager.Subscribe<ShipBulletEventData>(SpawnBullet);
        private void OnDisable()
            => EventManager.Unsubscribe<ShipBulletEventData>(SpawnBullet);
        private void SpawnBullet(ShipBulletEventData eventData)
        {
            Vector3 pos = eventData.position;
            var metaData = eventData.metaData;
            bulletEventData.metaData = metaData;
            switch (metaData.type)
            {
                case EShipBulletType.SingleBullet:
                    SpawnBullet(pos, new(0, metaData.speed, 0));
                    break;
                case EShipBulletType.DoubleBullet:
                    SpawnBullet(pos + new Vector3(-0.25f, 0, 0), new(0, metaData.speed, 0));
                    SpawnBullet(pos + new Vector3(0.25f, 0, 0), new(0, metaData.speed, 0));
                    break;
                case EShipBulletType.TripleBullet:
                    SpawnBullet(pos, new(-metaData.speed * Mathf.Sin(Mathf.PI / 12), metaData.speed, 0));
                    SpawnBullet(pos, new(0, metaData.speed, 0));
                    SpawnBullet(pos, new(metaData.speed * Mathf.Sin(Mathf.PI / 12), metaData.speed, 0));
                    break;
                case EShipBulletType.MissileBullet:
                    SpawnBullet(pos, new(0, metaData.speed, 0));
                    break;
            }
        }
        private void SpawnBullet(Vector3 pos, Vector3 velocity)
        {
            bulletEventData.velocity = velocity;
            var bullet = InstantiateItem(pos);
            bullet.data.SetData(bulletEventData);
        }
    }
}