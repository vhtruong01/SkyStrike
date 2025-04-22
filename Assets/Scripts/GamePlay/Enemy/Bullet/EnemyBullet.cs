using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBullet : PoolableObject<EnemyBulletData>
    {
        private float elapsedTime;
        private Vector3 velocity;

        public override void SetData(EnemyBulletData data)
        {
            base.SetData(data);
            elapsedTime = 0;
            transform.localScale = Vector3.one * data.size;
        }
        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, velocity));
        }
        public void Update()
        {
            elapsedTime += Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            if (elapsedTime >= data.lifeTime)
                Disappear();
        }
    }
}
