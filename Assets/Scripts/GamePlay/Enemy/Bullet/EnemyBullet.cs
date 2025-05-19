using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyBulletData))]
    public class EnemyBullet : PoolableObject<EnemyBulletData>, IDamager, IReflectable, IDestroyable
    {
        private SpriteAnimation anim;
        public EDamageType damageType => EDamageType.Normal;

        public override void Awake()
        {
            base.Awake();
            anim = GetComponent<SpriteAnimation>();
        }
        public override void Refresh()
        {
            anim.SetData(data.sprites);
            transform.localScale = Vector3.one * data.metaData.size;
            SetVelocity(data.velocity);
            //spriteRenderer.color = data.color;
        }
        private void Update()
        {
            if (!isActive) return;
            data.lifetime -= Time.deltaTime;
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
                        SetVelocity(new(cos * data.velocity.x - sin * data.velocity.y, sin * data.velocity.x + cos * data.velocity.y, 0));
                    }
                }
            }
            transform.position += data.velocity * Time.deltaTime;
            if (data.lifetime <= 0)
                Disappear();
        }
        public void AfterHit() => Disappear();
        public int GetDamage() => data.damage;
        public void Reflect(Vector2 normal)
        {
            float angle = Vector2.SignedAngle(normal, data.velocity) * 2 * Mathf.Deg2Rad;
            float sin = Mathf.Sin(Mathf.PI - angle);
            float cos = Mathf.Cos(Mathf.PI - angle);
            float x = data.velocity.x;
            float y = data.velocity.y;
            SetVelocity(new(x * cos - y * sin, x * sin + y * cos, data.velocity.z));
        }
        private void SetVelocity(Vector3 velocity)
        {
            data.velocity = velocity;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, data.velocity));
        }
    }
}