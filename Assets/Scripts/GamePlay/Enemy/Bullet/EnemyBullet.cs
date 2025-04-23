using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBullet : PoolableObject<EnemyBulletData>
    {
        private static readonly float maxViewAngle = 90;
        private static readonly float maxRotationAngle = 10;
        private static readonly float squaredMaxDistance = 4f;
        private static readonly float maxViewTime = 0.25f;
        private float elapsedTime;
        private Vector3 velocity;
        private bool isLookingAtPlayer;
        private float curViewTime;


        public override void SetData(EnemyBulletData data)
        {
            base.SetData(data);
            elapsedTime = 0;
            transform.localScale = Vector3.one * data.size;
            isLookingAtPlayer = data.isLookingAtPlayer;
            curViewTime = 0;
        }
        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, velocity));
        }
        public void SetColor(Color color) => spriteRenderer.color = color;
        public void Update()
        {
            elapsedTime += Time.deltaTime;
            if (isLookingAtPlayer)
            {
                curViewTime += Time.deltaTime;
                if (curViewTime >= maxViewTime)
                {
                    Vector2 shipDir = Ship.pos - transform.position;
                    float angle = Vector2.SignedAngle(velocity, shipDir);
                    if (angle < maxViewAngle && shipDir.sqrMagnitude <= squaredMaxDistance)
                    {
                        curViewTime = 0;
                        float rad = Mathf.Deg2Rad * Mathf.Sign(angle) * Mathf.Min(Mathf.Abs(angle), maxRotationAngle);
                        float sin = Mathf.Sin(rad);
                        float cos = Mathf.Cos(rad);
                        velocity = new Vector3(cos * velocity.x - sin * velocity.y, sin * velocity.x + cos * velocity.y, 0);
                    }
                }
            }
            transform.position += velocity * Time.deltaTime;
            if (elapsedTime >= data.lifeTime)
                Disappear();
        }
    }
}
