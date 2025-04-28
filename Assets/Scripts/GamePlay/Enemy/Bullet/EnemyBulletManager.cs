using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletManager : PoolManager<EnemyBullet, EnemyBulletData>
    {
        private readonly EnemyBulletData.EnemyBulletEventData bulletEventData = new();

        private void OnEnable()
            => EventManager.Subscribe<EnemyBulletData.EnemyBulletEventData>(SpawnBullet);
        private void OnDisable()
            => EventManager.Unsubscribe<EnemyBulletData.EnemyBulletEventData>(SpawnBullet);
        private void SpawnBullet(EnemyBulletData.EnemyBulletEventData eventData)
        {
            var metaData = bulletEventData.metaData = eventData.metaData;
            if (metaData == null || metaData.amount == 0) return;
            float unitAngle = metaData.isCircle
                              ? 2 * Mathf.PI / metaData.amount
                              : (Mathf.Deg2Rad * metaData.unitAngle);
            if (metaData.isCircle)
                SpawnCircle(eventData, unitAngle);
            else SpawnStraight(eventData, unitAngle);
        }
        private void SpawnCircle(EnemyBulletData.EnemyBulletEventData eventData, float unitAngle)
        {
            for (int i = 0; i < eventData.metaData.amount; i++)
            {
                bulletEventData.velocity = new(Mathf.Sin(eventData.angle + unitAngle * i) * eventData.metaData.velocity,
                                               -Mathf.Cos(eventData.angle + unitAngle * i) * eventData.metaData.velocity);
                bulletEventData.position = eventData.position + eventData.metaData.position.SetZ(0);
                var bullet = InstantiateItem(bulletEventData.position);
                bullet.data.SetData(bulletEventData);
            }
        }
        private void SpawnStraight(EnemyBulletData.EnemyBulletEventData eventData, float unitAngle)
        {
            float mid = 0.5f * (eventData.metaData.amount - 1);
            for (float i = -mid; i <= mid; i += 1f)
            {
                bulletEventData.velocity = new(Mathf.Sin(eventData.angle + unitAngle * i) * eventData.metaData.velocity,
                                               -Mathf.Cos(eventData.angle + unitAngle * i) * eventData.metaData.velocity);
                bulletEventData.position = eventData.position + (eventData.metaData.position + eventData.metaData.spacing * i).SetZ(0);
                var bullet = InstantiateItem(bulletEventData.position);
                bullet.data.SetData(bulletEventData);
            }
        }
    }
}