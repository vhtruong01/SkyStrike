using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(ShipBulletData))]
    public class ShipBullet : PoolableObject<ShipBulletData>, IBullet
    {
        public int GetDamage() => data.metaData.dmg;
        public override void Refresh()
        {
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.material = data.metaData.material;
            transform.localScale = Vector3.one * data.metaData.scale;
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, data.velocity));
        }
        public void Update()
        {
            data.timeLife -= Time.deltaTime;
            transform.position += data.velocity * Time.deltaTime;
            if (data.timeLife <= 0)
                Disappear();
        }
    }
}