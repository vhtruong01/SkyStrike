using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.Editor
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private BulletObject prefab;
        private ObjectPool<BulletObject> objectPool;
        private BulletDataObserver bulletData;
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
            if (bulletData == null) return;
            elaspedTime += Time.fixedDeltaTime;
            if (elaspedTime >= bulletData.timeCooldown.data)
            {
                if (bulletData.amount.data > 0)
                    Spawn();
                if (bulletData.isCircle.data )
                {
                    spawnerAngle += bulletData.spinSpeed.data * elaspedTime;
                    spawnerAngle %= 360;
                }
                elaspedTime = 0;
            }
        }
        private void Spawn()
        {
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
                float startAngle = bulletData.isLookingAtPlayer.data ? 0 : bulletData.startAngle.data;
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
            this.bulletData = bulletData;
            if (bulletData == null) return;
            elaspedTime = bulletData.timeCooldown.data;
            spawnerAngle = 0;
            for (int i = 0; i < transform.childCount; i++)
                if (transform.GetChild(i).gameObject.activeSelf)
                    objectPool.Release(transform.GetChild(i).GetComponent<BulletObject>());
        }
    }
}