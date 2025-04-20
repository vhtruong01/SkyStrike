using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : MonoBehaviour
    {
        private EnemyBulletData data;
        private float elaspedTime;
        private float angle;
        public bool isSpawn { get;set;}

        public void SetData(EnemyBulletData data)
        {
            this.data = data;
            if (data == null) return;
            isSpawn = true;
            elaspedTime = data.isStartAwake ? data.timeCooldown : 0;
            angle = data.isCircle ? 0 : data.startAngle;
        }
        public void Update()
        {
            if (!isSpawn ||  data == null) return;
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= data.timeCooldown)
            {
                if (data.isCircle)
                {
                    angle += data.spinSpeed * elaspedTime;
                    angle %= 360;
                }
                elaspedTime = 0;
                EventManager.SpawnEnemyBullet(data, transform.position, Mathf.Deg2Rad * angle);
            }
        }
    }
}