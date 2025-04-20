using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBullet : PoolableObject<ShipBulletData>
    {
        private float timeLife;
        private Vector3 velocity;

        public override void SetData(ShipBulletData data)
        {
            base.SetData(data);
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.material = data.metaData.material;
            timeLife = data.metaData.timeLife;
        }
        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, velocity));
        }
        public void Update()
        {
            if (timeLife <= 0)
            {
                Disappear();
                return;
            }
            timeLife -= Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }
}