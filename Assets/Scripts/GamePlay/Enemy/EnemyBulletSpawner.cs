using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyBulletSpawner : MonoBehaviour
        {
            private float elaspedTime;
            private EnemyBulletData data;
            private float angle;

            public void SetData(EnemyBulletData data)
            {
                this.data = data;
                if (data == null) return;
                elaspedTime = data.isStartAwake ? data.timeCooldown : 0;
                angle = data.isCircle ? 0 : data.startAngle;
            }
            public void Update()
            {
                if (data == null) return;
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
}