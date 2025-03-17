using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Ship
    {
        public class BulletManager : MonoBehaviour
        {
            [SerializeField] private List<IBulletSpawner> bulletSpawners;
            [SerializeField] private BulletSpawner defaultSpawner;

            public void Awake()
            {
                bulletSpawners = new();
                InstantiateSpawner(defaultSpawner);
            }
            public void InstantiateSpawner<T>(T spawnerPrefab) where T : Object, IBulletSpawner
            {
                var spawner = Instantiate(spawnerPrefab, transform, false);
                spawner.name = spawner.GetType().Name;
                bulletSpawners.Add(spawner);
            }
            public void Shoot()
            {
                foreach (IBulletSpawner spawner in bulletSpawners)
                    spawner.StartSpawn();
            }
            public void StopShoot()
            {
                foreach (IBulletSpawner spawner in bulletSpawners)
                    spawner.StopSpawn();
            }
            public void ChangeSpawner(IBulletSpawner spawner, int index)
            {
                if (index < 0 || index >= bulletSpawners.Count) return;
                bulletSpawners[index] = spawner;
            }
            public void RemoveSpawner(IBulletSpawner spawner, int index)
            {
                if (index < 0 || index >= bulletSpawners.Count) return;
                //
            }
            public void RestartSpawn()
            {
                //
            }
        }
    }
}