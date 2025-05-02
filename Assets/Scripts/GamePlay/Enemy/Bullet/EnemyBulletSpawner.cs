using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : MonoBehaviour, IEnemyComponent, ISpawnable
    {
        private readonly EnemyBulletData.EnemyBulletEventData bulletEventData = new();
        private float elaspedTime;
        private float angle;
        private SpriteAnimation anim;
        public IEntity entity { get; set; }
        public EnemyData enemyData { get; set; }

        public void Init()
            => anim = GetComponentInChildren<SpriteAnimation>(true);
        public void UpdateData()
            => anim.SetData(enemyData.metaData.weaponSprites);
        public void Spawn()
        {
            if (bulletEventData.metaData == enemyData.bulletData) return;
            if (enemyData.bulletData != null)
            {
                var metaData = bulletEventData.metaData = enemyData.bulletData;
                enemyData.isSpawn = true;
                elaspedTime = metaData.isStartAwake ? metaData.timeCooldown : 0;
                angle = metaData.isCircle ? 0 : metaData.startAngle;
            }
            else Stop();
        }
        public void Stop() => enemyData.isSpawn = false;
        public void Interrupt() => Stop();
        private void Update()
        {
            if (!enemyData.isSpawn) return;
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= bulletEventData.metaData.timeCooldown)
            {
                if (enemyData.isLookingAtPlayer)
                    angle = Vector2.SignedAngle(Vector2.down, Ship.pos - entity.position);
                else if (bulletEventData.metaData.isCircle)
                {
                    angle += bulletEventData.metaData.spinSpeed * elaspedTime;
                    angle %= 360;
                }
                elaspedTime = 0;
                bulletEventData.position = entity.position;
                bulletEventData.angle = Mathf.Deg2Rad * angle;
                //animation
                EventManager.Active(bulletEventData);
            }
        }
    }
}