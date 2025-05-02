using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class ShieldSkill : Skill<ShieldData>, IShipComponent
    {
        private Rigidbody2D rigi;

        public void Awake()
        {
            rigi = GetComponent<Rigidbody2D>();
            rigi.simulated = false;
        }
        private IEnumerator UseShield()
        {
            shipData.shield = true;
            anim.Play();
            rigi.simulated = true;
            yield return new WaitForSeconds(skillData.duration);
            shipData.shield = false;
            anim.Stop();
            rigi.simulated = false;
        }
        public override void Execute()
            => coroutine = StartCoroutine(UseShield());
        public override void Upgrade()
        {
            transform.localScale = Vector3.one * skillData.scale;
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet") && collision.TryGetComponent<IReflectable>(out var obj))
            {
                Vector2 normal = obj.gameObject.transform.position - transform.position;
                obj.Reflect(normal);
            }
        }
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet") && collision.TryGetComponent<IDestroyable>(out var obj))
            {
                obj.Disappear();
            }
        }
    }
}