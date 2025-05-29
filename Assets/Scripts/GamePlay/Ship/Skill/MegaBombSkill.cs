using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class MegaBombSkill : Skill<MegaBombData>, IDamager
    {
        private static readonly float maxSize = 25f;
        private Rigidbody2D rigi;
        protected override ESound upgradeSound => ESound.MegaBomb;
        public EDamageType damageType => skillData.damageType;

        public void Awake()
        {
            rigi = GetComponent<Rigidbody2D>();
            transform.localScale = Vector3.zero;
            rigi.simulated = false;
        }
        public override void Execute()
        {
            EventManager.Active(EEventType.StopTime);
            StartCoroutine(ClearBulletAndDealDmg());
        }
        private IEnumerator ClearBulletAndDealDmg()
        {
            float elapedTime = 0;
            float duration = 0.5f;
            SoundManager.PlaySound(ESound.Clock);
            while (elapedTime < duration)
            {
                elapedTime += Time.unscaledDeltaTime;
                transform.localScale = Vector3.one * (maxSize * elapedTime / duration);
                yield return null;
            }
            SoundManager.PlaySound(ESound.Clock);
            yield return new WaitForSecondsRealtime(0.5f);
            EventManager.Active(EEventType.ClearEnemyBullet);
            rigi.simulated = true;
            SoundManager.PlaySound(ESound.Explosion);
            elapedTime = 0f;
            while (elapedTime < duration)
            {
                elapedTime += Time.unscaledDeltaTime;
                transform.localScale = Vector3.one * (maxSize - maxSize * elapedTime / duration);
                yield return null;
            }
            rigi.simulated = false;
            transform.localScale = Vector3.zero;
        }
        protected override void UpgradeStat() { }
        public int GetDamage()
            => skillData.damage + Random.Range(-skillData.damage, skillData.damage) / 100;
        public void AfterHit() { }
    }
}