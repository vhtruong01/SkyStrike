using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : MonoBehaviour, IEnemyComponent, ISpawnable
    {
        private readonly EnemyBulletData.EnemyBulletEventData bulletEventData = new();
        private float elaspedTime;
        private float angle;
        public IEntity entity { get; set; }
        public EnemyData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Spawn()
        {
            if (bulletEventData.metaData == data.bulletData) return;
            if (data.bulletData != null)
            {
                var metaData = bulletEventData.metaData = data.bulletData;
                data.isSpawn = true;
                elaspedTime = metaData.isStartAwake ? metaData.timeCooldown : 0;
                angle = metaData.isCircle ? 0 : metaData.startAngle;
            }
            else data.isSpawn = false;
        }
        public void Stop() => data.isSpawn = false;
        public void Interrupt() => Stop();
        private void Update()
        {
            if (!data.isSpawn) return;
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= bulletEventData.metaData.timeCooldown)
            {
                if (data.isLookingAtPlayer)
                    angle = Vector2.SignedAngle(Vector2.down, Ship.pos - entity.position);
                else if (bulletEventData.metaData.isCircle)
                {
                    angle += bulletEventData.metaData.spinSpeed * elaspedTime;
                    angle %= 360;
                }
                elaspedTime = 0;
                bulletEventData.position = entity.position;
                bulletEventData.angle = Mathf.Deg2Rad * angle;
                EventManager.Active(bulletEventData);
            }
        }
    }
}