using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : MonoBehaviour, IEnemyComponent, ISpawnable
    {
        private readonly EnemyBulletEventData bulletEventData = new();
        private float elaspedTime;
        private float angle;
        private SpriteAnimation anim;
        public IObject entity { get; set; }
        public EnemyData enemyData { get; set; }

        public void Init()
            => anim = GetComponentInChildren<SpriteAnimation>(true);
        public void RefreshData()
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
                bulletEventData.asset = enemyData.metaData.bulletSprites;
                anim.SetDuration(enemyData.bulletData.timeCooldown).Restart();
            }
            else Stop();
        }
        public void Stop()
        {
            enemyData.isSpawn = false;
            anim.Stop();
        }
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
                EventManager.Active(bulletEventData);
            }
        }
    }
}