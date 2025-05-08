namespace SkyStrike.Game
{
    public class SlowTimeSkill : Skill<ActivateSkillData>
    {
        public override void Execute()
            => EventManager.Active(EEventType.SlowTime);
        public override void Upgrade() { }
    }
}