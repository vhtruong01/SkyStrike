using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class ShieldSkill : Skill<ShieldData>, IShipComponent
    {
        private AlphaValueAnimation alphaValueAnimation;
        private Rigidbody2D rigi;

        public override void Init()
        {
            alphaValueAnimation = GetComponent<AlphaValueAnimation>();
            base.Init();
            rigi = GetComponent<Rigidbody2D>();
            rigi.simulated = false;
        }
        private IEnumerator UseShield()
        {
            shipData.shield = true;
            anim.Play();
            alphaValueAnimation.Restart();
            rigi.simulated = true;
            SoundManager.PlaySound(ESound.ShieldStart);
            yield return new WaitForSeconds(skillData.duration-1.5f);
            SoundManager.PlaySound(ESound.ShieldEnd);
            yield return new WaitForSeconds(1.5f);
            shipData.shield = false;
            anim.Stop();
            rigi.simulated = false;
            coroutine = null;
        }
        public override void Execute()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(UseShield());
        }
        protected override void UpgradeStat()
        {
            transform.localScale = Vector3.one * skillData.scale;
            alphaValueAnimation.SetDuration(skillData.duration);
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet") && collision.TryGetComponent<IReflectable>(out var obj))
            {
                Vector2 normal = obj.position - transform.position;
                obj.Reflect(normal);
            }
        }
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet") && collision.TryGetComponent<IDestroyable>(out var obj))
                obj.Disappear();
        }
    }
}