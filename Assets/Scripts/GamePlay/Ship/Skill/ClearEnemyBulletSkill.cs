using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class ClearEnemyBulletSkill : Skill<ActivateSkillData>
    {
        private static float maxSize = 20;
        private Rigidbody2D rigi;

        public void Awake()
        {
            rigi = GetComponent<Rigidbody2D>();
            transform.localScale = Vector3.zero;
            rigi.simulated = false;
        }
        public override void Execute()
        {
            EventManager.Active(EEventType.StopTime);
            StartCoroutine(ClearBullet());
        }
        private IEnumerator ClearBullet()
        {
            rigi.simulated = true;
            float duration = 1f;
            float elapedTime = 0;
            while (elapedTime < duration)
            {
                elapedTime += Time.unscaledDeltaTime;
                transform.localScale = Vector3.one * (maxSize * elapedTime / duration);
                yield return null;
            }
            rigi.simulated = false;
            elapedTime = 0f;
            duration = 0.5f;
            while (elapedTime < duration)
            {
                elapedTime += Time.unscaledDeltaTime;
                transform.localScale = Vector3.one * (maxSize - maxSize * elapedTime / duration);
                yield return null;
            }
            transform.localScale = Vector3.zero;
        }
        public override void Upgrade() { }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet") && collision.TryGetComponent(out IDestroyable bullet))
                bullet.Disappear();
        }
    }
}