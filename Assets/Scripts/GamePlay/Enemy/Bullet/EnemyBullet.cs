using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyBulletData))]
    public class EnemyBullet : PoolableObject<EnemyBulletData>, IDamager
    {
        public override void Refresh()
        {
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, data.velocity));
            transform.localScale = Vector3.one * data.metaData.size;
            spriteRenderer.color = data.color;
        }
        private void Update()
        {
            data.elapsedTime += Time.deltaTime;
            if (data.isLookingAtPlayer)
            {
                data.curViewTime += Time.deltaTime;
                if (data.curViewTime >= EnemyBulletData.maxViewTime)
                {
                    Vector2 shipDir = Ship.pos - transform.position;
                    float angle = Vector2.SignedAngle(data.velocity, shipDir);
                    if (angle < EnemyBulletData.maxViewAngle && shipDir.sqrMagnitude <= EnemyBulletData.squaredMaxDistance)
                    {
                        data.curViewTime = 0;
                        float rad = Mathf.Deg2Rad * Mathf.Sign(angle) * Mathf.Min(Mathf.Abs(angle), EnemyBulletData.maxRotationAngle);
                        float sin = Mathf.Sin(rad);
                        float cos = Mathf.Cos(rad);
                        data.velocity = new Vector3(cos * data.velocity.x - sin * data.velocity.y, sin * data.velocity.x + cos * data.velocity.y, 0);
                    }
                }
            }
            transform.position += data.velocity * Time.deltaTime;
            if (data.elapsedTime >= data.metaData.lifeTime)
                Disappear();
        }
        public void AfterHit() => Disappear();
        public int GetDamage() => data.damage;
    }
}