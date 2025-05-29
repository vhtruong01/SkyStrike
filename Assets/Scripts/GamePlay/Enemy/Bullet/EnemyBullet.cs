using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyBulletData))]
    public class EnemyBullet : PoolableObject<EnemyBulletData>, IDamager, IReflectable, IDestroyable
    {
        private static readonly float sqrDistance = 1f;
        private SpriteAnimation anim;
        public EDamageType damageType => EDamageType.Normal;

        public override void Awake()
        {
            base.Awake();
            anim = GetComponent<SpriteAnimation>();
        }
        public override void Refresh()
        {
            anim.SetData(data.asset.sprites);
            transform.localScale = Vector3.one * data.metaData.size;
            col2D.size = data.asset.sprites[0].bounds.size * 0.75f;
            //spriteRenderer.color = data.color;
        }
        private void Update()
        {
            if (!isActive) return;
            if (IsNearPlayer())
                EnableCollider(true);
            else EnableCollider(false);
            float deltaTime = Time.deltaTime;
            data.elapsedTime += deltaTime;
            float remainTime = data.stateDuration - data.elapsedTime;
            if (remainTime > 0)
            {
                if (remainTime >= data.transitionDuration)
                    transform.position += data.velocity * (data.defaultSpeed * data.startCoef * deltaTime);
                else transform.position += data.velocity * (data.defaultSpeed * Lerp(data.endCoef, data.startCoef, remainTime / data.transitionDuration) * deltaTime);
                if (data.endScale != data.startScale)
                    transform.localScale = Vector3.one * Lerp(data.startScale, data.endScale, data.elapsedTime / data.stateDuration);
            }
            else if (!data.ChangeState())
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
            data.SetVelocityAndCancelState(new(x * cos - y * sin, x * sin + y * cos, data.velocity.z));
        }
        private float Lerp(float a, float b, float t)
            => a + (b - a) * t;
        private bool IsNearPlayer()
        {
            float num = transform.position.x - Ship.pos.x;
            float num2 = transform.position.y - Ship.pos.y;
            return num * num + num2 * num2 < sqrDistance;
        }
    }
}