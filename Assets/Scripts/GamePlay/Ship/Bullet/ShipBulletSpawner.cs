using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Game
    {
        public class ShipBulletSpawner : MonoBehaviour
        {
            private bool isSpawn;
            private float elaspedTime;
            public ObjectPool<ShipBullet> bulletPool { private get; set; }
            public ShipBulletData bulletData { private get; set; }

            public void Awake()
            {
                isSpawn = true;
            }
            public void Update()
            {
                if (!isSpawn) return;
                elaspedTime += Time.deltaTime;
                if (elaspedTime >= bulletData.timeCooldown)
                {
                    Spawn();
                    elaspedTime = 0;
                }
            }
            public virtual void Spawn()
            {
                ShipBullet bullet = bulletPool.Get();
                bullet.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                bullet.SetData(bulletData);
                //
            }
            public void Upgrade()
            {
                ///
            }
        }
    }
}