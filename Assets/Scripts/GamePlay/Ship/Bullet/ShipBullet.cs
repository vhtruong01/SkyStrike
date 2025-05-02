using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipBullet : PoolableObject<ShipBulletData>, IDamager
    {
        public EDamageType damageType => data.damageType;

        public override void Refresh()
        {
            spriteRenderer.sprite = data.metaData.sprite;
            col2D.size = data.metaData.sprite.bounds.size;
            transform.localScale = Vector3.one * data.metaData.scale;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, data.velocity));
            spriteRenderer.sharedMaterial = data.metaData.material;
        }
        private void Update()
        {
            data.lifetime -= Time.deltaTime;
            transform.position += data.velocity * Time.deltaTime;
            if (data.lifetime <= 0)
                Disappear();
        }
        public int GetDamage() => data.metaData.dmg;
        public void AfterHit() => Disappear();
    }
}