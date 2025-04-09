using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ShipBullet : PoolableObject
        {
            private float timeLife;
            private float speed;
            private int dmg;

            public void SetData(ShipBulletData data)
            {
                timeLife = data.timeLife;
                speed = data.speed;
                spriteRenderer.sprite = data.sprite;
                dmg = data.dmg;
            }
            public void Update()
            {
                if (timeLife <= 0)
                {
                    Release();
                    return;
                }
                timeLife -= Time.deltaTime;
                transform.Translate(speed * Time.deltaTime * Vector3.up);
            }
            public override void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.GetComponentInParent<Enemy>();
                    enemy.TakeDamage(dmg);
                }
            }
        }
    }
}