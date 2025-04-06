using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Ship
    {
        public abstract class BulletSpawner : MonoBehaviour, IBulletSpawner
        {
            [SerializeField] protected BulletData bulletData;
            [SerializeField] protected Bullet bulletPrefab;
            protected Entity ship;
            protected IEnumerator enumerator;
            protected ObjectPool<Bullet> bulletPool;
            protected GameObject bulletContainer;
            protected WaitForSeconds waitForSeconds;

            public void Awake()
            {
                ship = GetComponentInParent<Entity>();
                bulletPool = new(CreateBullet, GetBullet, ReleaseBullet);
                bulletContainer = new GameObject(bulletData.type + "Container");
                waitForSeconds = new(bulletData.timeCountdown);
            }
            protected Bullet CreateBullet()
            {
                Bullet bullet = Instantiate(bulletPrefab);
                bullet.name = bulletData.type;
                //bullet.SetParent(ship);
                bullet.transform.SetParent(bulletContainer.transform, false);
                bullet.onDestroy.AddListener(bulletPool.Release);
                return bullet;
            }
            protected void GetBullet(Bullet bullet)
            {
                bullet.gameObject.SetActive(true);
                bullet.SetTimeLife(bulletData.timeLife);
            }
            protected void ReleaseBullet(Bullet bullet) => bullet.gameObject.SetActive(false);
            public abstract void Spawn();
            public virtual void StartSpawn()
            {
                StopSpawn();
                enumerator = SpawnEnumerator();
                StartCoroutine(enumerator);
            }
            public virtual void StopSpawn()
            {
                if (enumerator != null)
                    StopCoroutine(enumerator);
                enumerator = null;
            }
            protected IEnumerator SpawnEnumerator()
            {
                yield return waitForSeconds;
                while (true)
                {
                    Spawn();
                    yield return waitForSeconds;
                }
            }
        }
    }
}