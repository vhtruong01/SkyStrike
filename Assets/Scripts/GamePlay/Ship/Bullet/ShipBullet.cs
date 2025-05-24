using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBullet : PoolableObject<ShipBulletData>, IDamager
    {
        private IDamageable target;
        public EDamageType damageType => data.metaData.damageType;

        public override void Refresh()
        {
            spriteRenderer.sprite = data.metaData.sprite;
            col2D.size = data.metaData.sprite.bounds.size;
            transform.localScale = Vector3.one * data.metaData.scale;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, data.velocity));
            spriteRenderer.sharedMaterial = data.metaData.material;
            if (data.metaData.type == EShipBulletType.MissileBullet)
                FindTarget();
            else target = null;
        }
        private void Update()
        {
            if (!isActive) return;
            data.lifetime -= Time.deltaTime;
            if (target != null)
            {
                if (target.isActive)
                {
                    data.angularVelocity = Vector3.Cross(transform.up, target.position - transform.position).normalized.z;
                    transform.eulerAngles += new Vector3(0, 0, data.angularVelocity * Time.deltaTime * 135);
                }
                else data.lifetime = 0;
            }
            transform.Translate(data.velocity * Time.deltaTime);
            if (data.lifetime <= 0)
                Disappear();
        }
        private void FindTarget()
        {
            var hit = Physics2D.CircleCast(new(), 8f, Vector2.up, 0, LayerMask.GetMask("Enemy"));
            if (hit.collider != null)
                target = hit.collider.GetComponent<IDamageable>();
        }
        public int GetDamage()
        {
            var damage = data.metaData.dmg;
            return damage + Random.Range(-damage, damage) / 20;
        }
        public void AfterHit()
        {
            if (damageType != EDamageType.Slashing)
                Disappear();
        }
    }
}