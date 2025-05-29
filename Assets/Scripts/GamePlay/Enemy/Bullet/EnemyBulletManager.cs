using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletManager : PoolManager<EnemyBullet, EnemyBulletData>
    {
        private readonly EnemyBulletEventData bulletEventData = new();

        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<EnemyBulletEventData>(SpawnBullet);
            EventManager.Subscribe(EEventType.ClearEnemyBullet, DestroyAll );
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<EnemyBulletEventData>(SpawnBullet);
            EventManager.Unsubscribe(EEventType.ClearEnemyBullet, DestroyAll);
        }
        private void SpawnBullet(EnemyBulletEventData eventData)
        {
            var metaData = bulletEventData.metaData = eventData.metaData;
            bulletEventData.asset = eventData.asset;
            if (metaData == null || metaData.amount == 0) return;
            float unitAngle = metaData.isCircle
                              ? 2 * Mathf.PI / metaData.amount
                              : (Mathf.Deg2Rad * metaData.unitAngle);
            if (metaData.isCircle)
                SpawnCircle(eventData, unitAngle);
            else SpawnStraight(eventData, unitAngle);
        }
        private void SpawnCircle(EnemyBulletEventData eventData, float unitAngle)
        {
            for (int i = 0; i < eventData.metaData.amount; i++)
            {
                bulletEventData.velocity = new Vector3(Mathf.Sin(eventData.angle + unitAngle * i),
                                               -Mathf.Cos(eventData.angle + unitAngle * i));
                bulletEventData.position = eventData.position + eventData.metaData.position.SetZ(0);
                var bullet = InstantiateItem(bulletEventData.position);
                bullet.data.SetData(bulletEventData);
            }
        }
        private void SpawnStraight(EnemyBulletEventData eventData, float unitAngle)
        {
            float mid = 0.5f * (eventData.metaData.amount - 1);
            for (float i = -mid; i <= mid; i += 1f)
            {
                bulletEventData.velocity = eventData.velocity + new Vector3(Mathf.Sin(eventData.angle + unitAngle * i),
                                               -Mathf.Cos(eventData.angle + unitAngle * i));
                bulletEventData.position = eventData.position + (eventData.metaData.position + eventData.metaData.spacing * i).SetZ(0);
                var bullet = InstantiateItem(bulletEventData.position);
                bullet.data.SetData(bulletEventData);
            }
        }
    }
}