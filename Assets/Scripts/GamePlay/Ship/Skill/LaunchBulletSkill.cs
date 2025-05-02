namespace SkyStrike.Game
{
    public class LaunchBulletSkill : Skill<BulletData>
    {
        private readonly ShipBulletData.ShipBulletEventData bulletEventData = new();

        public override void Init()
        {
            base.Init();
            bulletEventData.metaData = skillData;
        }
        public override void AutoActive()
        {
            if (shipData.isSpawn)
            {
                Execute();
                skillData.elapsedTime = 0;
            }
        }
        public override void Execute()
        {
            bulletEventData.position = transform.position;
            EventManager.Active(bulletEventData);
            anim.Restart();
        }
        public override void Upgrade()
            => anim.SetDuration(skillData.cooldown);
    }
}