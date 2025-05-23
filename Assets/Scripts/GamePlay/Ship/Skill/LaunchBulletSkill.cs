using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class LaunchBulletSkill : Skill<BulletData>
    {
        private readonly ShipBulletEventData bulletEventData = new();
        private WaitForSeconds waitForSeconds;

        public override void Init()
        {
            base.Init();
            bulletEventData.metaData = skillData;
        }
        public override void AutoActive()
        {
            if (shipData.isSpawn)
                Execute();
        }
        public override void Execute()
        {
            skillData.elapsedTime = 0;
            coroutine = StartCoroutine(Fire());
        }
        private IEnumerator Fire()
        {
            anim.Restart();
            yield return waitForSeconds;
            if (shipData.isSpawn)
            {
                bulletEventData.position = transform.position;
                EventManager.Active(bulletEventData);
            }
            coroutine = null;
        }
        protected override void UpgradeStat()
        {
            anim.SetDuration(skillData.cooldown);
            waitForSeconds = new WaitForSeconds(skillData.cooldown);
        }
    }
}