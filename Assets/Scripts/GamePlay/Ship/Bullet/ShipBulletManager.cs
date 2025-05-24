using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBulletManager : PoolManager<ShipBullet, ShipBulletData>
    {
        private readonly ShipBulletEventData bulletEventData = new();

        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<ShipBulletEventData>(SpawnBullet);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<ShipBulletEventData>(SpawnBullet);
        }
        private void SpawnBullet(ShipBulletEventData eventData)
        {
            Vector3 pos = eventData.position;
            var metaData = eventData.metaData;
            bulletEventData.metaData = metaData;
            switch (metaData.type)
            {
                case EShipBulletType.SingleBullet:
                    SpawnBullet(pos, new(0, metaData.speed, 0));
                    SoundManager.PlaySound(ESound.SingleBullet);
                    break;
                case EShipBulletType.MiniBullet:
                    SpawnBullet(pos + new Vector3(-0.1f, 0), new(-0.075f, metaData.speed, 0));
                    SpawnBullet(pos + new Vector3(0.1f, 0), new(0.075f, metaData.speed, 0));
                    break;
                case EShipBulletType.DoubleBullet:
                    SpawnBullet(pos + new Vector3(-0.33f, 0, 0), new(0, metaData.speed, 0));
                    SpawnBullet(pos + new Vector3(0.33f, 0, 0), new(0, metaData.speed, 0));
                    SoundManager.PlaySound(ESound.DoubleBullet);
                    break;
                case EShipBulletType.TripleBullet:
                    SpawnBullet(pos, new(-metaData.speed * Mathf.Sin(Mathf.PI / 24), metaData.speed, 0));
                    SpawnBullet(pos, new(-metaData.speed * Mathf.Sin(Mathf.PI / 36), metaData.speed, 0));
                    SpawnBullet(pos, new(-metaData.speed * Mathf.Sin(Mathf.PI / 72), metaData.speed, 0));
                    SpawnBullet(pos, new(metaData.speed * Mathf.Sin(Mathf.PI / 72), metaData.speed, 0));
                    SpawnBullet(pos, new(metaData.speed * Mathf.Sin(Mathf.PI / 36), metaData.speed, 0));
                    SpawnBullet(pos, new(metaData.speed * Mathf.Sin(Mathf.PI / 24), metaData.speed, 0));
                    SoundManager.PlaySound(ESound.TripleBullet);
                    break;
                case EShipBulletType.MissileBullet:
                    SpawnBullet(pos, new(0, metaData.speed, 0));
                    SoundManager.PlaySound(ESound.Missile);
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