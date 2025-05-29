using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletSpawner : MonoBehaviour, IEnemyComponent, ISpawnable
    {
        private readonly EnemyBulletEventData bulletEventData = new();
        private float elapsedTime;
        private float angle;
        private int stack;
        private float delay;
        private SpriteAnimation anim;
        public IObject entity { get; set; }
        public EnemyData enemyData { get; set; }
        private EnemyBulletMetaData metaData;

        public void Init()
        {
            anim = GetComponentInChildren<SpriteAnimation>(true);
        }
        public void RefreshData()
            => anim.SetData(enemyData.metaData.weaponSprites);
        public void Spawn()
        {
            if (enemyData.bulletData != null)
            {
                enemyData.isSpawn = true;
                if (metaData != enemyData.bulletData)
                {
                    metaData = bulletEventData.metaData = enemyData.bulletData;
                    angle = metaData.isCircle ? 0 : metaData.startAngle;
                    bulletEventData.asset = enemyData.metaData.bulletSprites;
                    anim.SetDuration(Mathf.Max(metaData.delay, metaData.timeCooldown)).Restart();
                    if (metaData.isStartAwake)
                    {
                        stack = metaData.stack;
                        delay = metaData.delay;
                        elapsedTime = metaData.timeCooldown;
                    }
                    else
                    {
                        stack = 0;
                        delay = 0;
                        elapsedTime = 0;
                    }
                }
            }
            else
            {
                Stop();
                metaData = null;
            }
        }
        public void Stop()
        {
            enemyData.isSpawn = false;
            anim.Stop();
        }
        public void Interrupt() => Stop();
        private void Update()
        {
            if (!enemyData.isSpawn || metaData.stack <= 0) return;
            if (stack <= 0 && metaData.delay > 0)
            {
                delay += Time.deltaTime;
                if (delay < metaData.delay) return;
                else
                {
                    stack = metaData.stack;
                    delay = 0;
                    elapsedTime = Mathf.Max(0, metaData.delay - metaData.timeCooldown);
                }
            }
            if (elapsedTime >= metaData.timeCooldown)
            {
                stack--;
                if (enemyData.isLookingAtPlayer)
                    angle = Vector2.SignedAngle(Vector2.down, Ship.pos - entity.position);
                else if (metaData.isCircle)
                {
                    angle += metaData.spinSpeed * Mathf.Max(Time.deltaTime, metaData.timeCooldown);
                    angle %= 360;
                }
                elapsedTime = 0;
                bulletEventData.position = entity.position;
                bulletEventData.angle = Mathf.Deg2Rad * angle;
                EventManager.Active(bulletEventData);
            }
            elapsedTime += Time.deltaTime;
        }
    }
}