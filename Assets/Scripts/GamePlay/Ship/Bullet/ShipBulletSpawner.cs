namespace SkyStrike.Game
{
    public sealed class ShipBulletSpawner : Skill<ActivateSkillData>, ISpawnable
    {
        public void Spawn() => shipData.isSpawn = true;
        public void Stop() => shipData.isSpawn = false;

        public override void Execute()
            => shipData.isSpawn = !shipData.isSpawn;
        protected override void UpgradeStat() { }
        public override void Interrupt()
        {
            base.Interrupt();
            Stop();
        }
    }
}