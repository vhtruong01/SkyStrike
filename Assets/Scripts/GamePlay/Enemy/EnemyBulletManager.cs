using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyBulletManager : PoolManager<EnemyBullet, EnemyBulletData>
        {
            public override void Awake()
            {
                base.Awake();
                EventManager.onSpawnEnemyBullet.AddListener(SpawnBullet);
            }
            public void SpawnBullet(EnemyBulletData data, Vector3 position, float angle)
            {
                if (data == null || data.amount == 0) return;
                EnemyBullet bullet;
                if (data.isCircle)
                {
                    float unitAngle = 2 * Mathf.PI / data.amount;
                    for (int i = 0; i < data.amount; i++)
                    {
                        bullet = InstantiateItem(data, position + data.position.SetZ(0));
                        bullet.SetVelocity(new Vector3(Mathf.Sin(angle + unitAngle * i), -Mathf.Cos(angle + unitAngle * i), 0)*data.velocity);
                    }
                }
                else
                {
                    float mid = 0.5f * (data.amount - 1);
                    for (float i = -mid; i <= mid; i += 1f)
                    {
                        bullet = InstantiateItem(data, position + (data.position + data.spacing * i).SetZ(0));
                        bullet.SetVelocity(new Vector3(Mathf.Sin(angle + data.unitAngle * i), -Mathf.Cos(angle + data.unitAngle * i), 0)*data.velocity);
                    }
                }
            }
        }
    }
}