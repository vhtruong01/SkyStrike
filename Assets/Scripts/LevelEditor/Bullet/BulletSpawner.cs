using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.Editor
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private BulletObject prefab;
        private ObjectPool<BulletObject> objectPool;
        private BulletDataObserver bulletData;
        private float elapsedTime;
        private float spawnerAngle;
        private Vector2 originalPos;
        private int stack;
        private float delay;

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
            if (bulletData == null || bulletData.stack.data <= 0) return;
            if (stack <= 0 && bulletData.delay.data > 0)
            {
                delay += Time.fixedDeltaTime;
                if (delay < bulletData.delay.data) return;
                else
                {
                    stack = bulletData.stack.data;
                    delay = 0;
                    elapsedTime = Mathf.Max(0, bulletData.delay.data - bulletData.timeCooldown.data);
                }
            }
            if (elapsedTime >= bulletData.timeCooldown.data)
            {
                stack--;
                Spawn();
                if (bulletData.isCircle.data)
                {
                    spawnerAngle += bulletData.spinSpeed.data * Mathf.Max(Time.fixedDeltaTime, bulletData.timeCooldown.data);
                    spawnerAngle %= 360;
                }
                elapsedTime = 0;
            }
            elapsedTime += Time.fixedDeltaTime;
        }
        private void Spawn()
        {
            if (bulletData.amount.data <= 0) return;
            if (bulletData.isCircle.data)
            {
                float unitAngle = 360 / bulletData.amount.data;
                for (int i = 0; i < bulletData.amount.data; i++)
                {
                    var bullet = objectPool.Get();
                    float angle = Mathf.Deg2Rad * (unitAngle * i + spawnerAngle);
                    Vector2 dir = new(Mathf.Sin(angle), -Mathf.Cos(angle));
                    bullet.Init(bulletData, dir, originalPos + bulletData.position.data);
                }
            }
            else
            {
                float startAngle = bulletData.startAngle.data;
                float mid = 0.5f * (bulletData.amount.data - 1);
                for (float i = -mid; i <= mid; i += 1f)
                {
                    var bullet = objectPool.Get();
                    bullet.Init(bulletData,
                        new(Mathf.Sin((startAngle + bulletData.unitAngle.data * i) * Mathf.Deg2Rad),
                        -Mathf.Cos((startAngle + bulletData.unitAngle.data * i) * Mathf.Deg2Rad)),
                        originalPos + bulletData.position.data + bulletData.spacing.data * i);
                }
            }
        }
        public void ChangeBulletSpawner(BulletDataObserver bulletData)
        {
            for (int i = 0; i < transform.childCount; i++)
                if (transform.GetChild(i).gameObject.activeSelf)
                    objectPool.Release(transform.GetChild(i).GetComponent<BulletObject>());
            this.bulletData = bulletData;
            if (bulletData == null) return;
            spawnerAngle = 0;
            stack = 0;
            elapsedTime = bulletData.timeCooldown.data;
            delay = bulletData.delay.data;
        }
    }
}