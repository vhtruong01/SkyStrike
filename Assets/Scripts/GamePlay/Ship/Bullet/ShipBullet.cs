using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ShipBullet : PoolableObject<ShipBulletData>
        {
            private float timeLife;
            private int dmg;
            private Vector3 speed;

            public override void SetData(ShipBulletData data)
            {
                spriteRenderer.sprite = data.metaData.sprite;
                spriteRenderer.material = data.metaData.material;
                timeLife = data.metaData.timeLife;
                dmg = data.dmg;
            }
            public void SetSpeed(Vector3 speed)
            {
                this.speed = speed;
                transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, speed));
            }
            public void Update()
            {
                if (timeLife <= 0)
                {
                    Release();
                    return;
                }
                timeLife -= Time.deltaTime;
                transform.position += speed * Time.deltaTime;
            }
            public override void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.GetComponent<Enemy>();
                    if (enemy.TakeDamage(dmg))
                        Release();
                }
            }
        }
    }
}