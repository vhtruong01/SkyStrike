using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletManager : PoolManager<EnemyBullet, EnemyBulletData>
    {
        public override void Awake()
        {
            base.Awake();
            EventManager.onSpawnEnemyBullet.AddListener(SpawnBullet);
        }
        public void SpawnBullet(EnemyBulletMetaData metaData, Vector3 position, float angle)
        {
            if (metaData == null || metaData.amount == 0) return;
            float unitAngle = metaData.isCircle ? 2 * Mathf.PI / metaData.amount : (Mathf.Deg2Rad * metaData.unitAngle);
            if (metaData.isCircle)
            {
                for (int i = 0; i < metaData.amount; i++)
                {
                    Vector3 velocity = new(Mathf.Sin(angle + unitAngle * i) * metaData.velocity,
                                           -Mathf.Cos(angle + unitAngle * i) * metaData.velocity);
                    CreateBullet(position + metaData.position.SetZ(0), velocity, metaData);
                }
            }
            else
            {
                float mid = 0.5f * (metaData.amount - 1);
                for (float i = -mid; i <= mid; i += 1f)
                {
                    Vector3 velocity = new(Mathf.Sin(angle + unitAngle * i) * metaData.velocity,
                                           -Mathf.Cos(angle + unitAngle * i) * metaData.velocity);
                    CreateBullet(position + (metaData.position + metaData.spacing * i).SetZ(0), velocity, metaData);
                }
            }
        }
        private void CreateBullet(Vector3 position, Vector3 velocity, EnemyBulletMetaData metaData)
        {
            var bullet = InstantiateItem(position);
            bullet.data.SetExtraData(velocity);
            bullet.data.UpdateDataAndRefresh(metaData);
        }
    }
}