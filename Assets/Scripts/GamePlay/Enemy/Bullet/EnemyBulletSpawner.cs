using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : EnemyComponent
    {
        private float elaspedTime;
        private float angle;
        private bool isSpawn;
        private EnemyBulletData bulletData;

        public override void Interrupt() => isSpawn = false;
        public void Spawn()
        {
            bulletData = data.bulletData;
            if (bulletData == null) return;
            isSpawn = true;
            elaspedTime = bulletData.isStartAwake ? bulletData.timeCooldown : 0;
            angle = bulletData.isCircle ? 0 : bulletData.startAngle;
        }
        public void Update()
        {
            if (!isSpawn || bulletData == null) return;
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= bulletData.timeCooldown)
            {
                if (bulletData.isCircle)
                {
                    angle += bulletData.spinSpeed * elaspedTime;
                    angle %= 360;
                }
                elaspedTime = 0;
                EventManager.SpawnEnemyBullet(bulletData, transform.position, Mathf.Deg2Rad * angle);
            }
        }
    }
}