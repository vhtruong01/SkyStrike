using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletSpawner : MonoBehaviour
        {
            private const float TAU = Mathf.PI * 2;
            [SerializeField] private BulletObject prefab;
            private BulletDataObserver bulletData;
            private ObjectPool<BulletObject> objectPool;
            private float elaspedTime;
            private float spawnerAngle;
            private Vector2 originalPos;

            public void Awake()
            {
                objectPool = new(CreateBullet, GetBullet, ReleaseBullet);
                originalPos = transform.position;
            }
            private BulletObject CreateBullet()
            {
                var bullet = Instantiate(prefab, transform, false);
                bullet.name = "Bullet";
                bullet.Init();
                bullet.onDestroy.AddListener(objectPool.Release);
                return bullet;
            }
            private void GetBullet(BulletObject bullet) => bullet.gameObject.SetActive(true);
            private void ReleaseBullet(BulletObject bullet) => bullet.gameObject.SetActive(false);
            public void FixedUpdate()
            {
                if (bulletData == null || bulletData.timeCooldown.data == 0) return;
                elaspedTime += Time.fixedDeltaTime;
                if (elaspedTime >= bulletData.timeCooldown.data)
                {
                    if (bulletData.amount.data > 0)
                        Spawn();
                    if (bulletData.isCircle.data && bulletData.spinSpeed.data > 0)
                    {
                        spawnerAngle += TAU / 360 * bulletData.spinSpeed.data * elaspedTime;
                        if (spawnerAngle >= TAU)
                            spawnerAngle -= TAU;
                    }
                    elaspedTime = 0;
                }
            }
            private void Spawn()
            {
                if (bulletData.isCircle.data)
                {
                    float unitAngle = TAU / bulletData.amount.data;
                    for (int i = 0; i < bulletData.amount.data; i++)
                    {
                        var bullet = objectPool.Get();
                        float angle = unitAngle * i + spawnerAngle;
                        Vector2 dir = new(Mathf.Cos(angle), Mathf.Sin(angle));
                        bullet.Init(bulletData, dir, originalPos + bulletData.position.data);
                    }
                }
                else
                {
                    float startAngle = bulletData.isLookAtPlayer.data ? 0 : (TAU / 360 * bulletData.startAngle.data);
                    float mid = 0.5f * (bulletData.amount.data - 1);
                    for (float i = -mid; i <= mid; i += 1f)
                    {
                        var bullet = objectPool.Get();
                        bullet.Init(bulletData,
                            new(Mathf.Sin(startAngle + bulletData.angleUnit.data * i * TAU / 360), -Mathf.Cos(startAngle + bulletData.angleUnit.data * i * TAU / 360)),
                            originalPos + bulletData.spacing.data * i);
                    }
                }
            }
            public void ChangeBulletSpawner(BulletDataObserver bulletData)
            {
                this.bulletData = bulletData;
                elaspedTime = 0;
                spawnerAngle = 0;
            }
        }
    }
}