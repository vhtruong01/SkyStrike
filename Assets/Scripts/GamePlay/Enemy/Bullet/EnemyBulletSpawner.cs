using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : MonoBehaviour, IEnemyComponent, ISpawnable
    {
        private float elaspedTime;
        private float angle;
        private bool isSpawn;
        private EnemyBulletMetaData bulletData;
        public EnemyData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Awake()
            => data = GetComponent<EnemyData>();
        public void Interrupt() => Stop();
        public void Spawn()
        {
            bulletData = data.bulletData;
            if (bulletData == null) return;
            isSpawn = true;
            elaspedTime = bulletData.isStartAwake ? bulletData.timeCooldown : 0;
            angle = bulletData.isCircle ? 0 : bulletData.startAngle;
        }
        public void Stop()
        {
            isSpawn = false;
            bulletData = null;
        }
        public void Update()
        {
            if (!isSpawn) return;
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= bulletData.timeCooldown)
            {
                if (data.isLookingAtPlayer)
                    angle = Vector2.SignedAngle(Vector2.down, Ship.pos - transform.position);
                else
                {
                    if (bulletData.isCircle)
                    {
                        angle += bulletData.spinSpeed * elaspedTime;
                        angle %= 360;
                    }
                }
                elaspedTime = 0;
                EventManager.SpawnEnemyBullet(bulletData, transform.position, Mathf.Deg2Rad * angle);
            }
        }
    }
}