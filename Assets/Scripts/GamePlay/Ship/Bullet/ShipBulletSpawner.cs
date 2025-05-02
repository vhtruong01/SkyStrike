namespace SkyStrike.Game
{
    public class ShipBulletSpawner : Skill<ActivateSkillData>, ISpawnable
    {
        public void Spawn() => shipData.isSpawn = true;
        public void Stop() => shipData.isSpawn = false;

        public override void Execute()
            => shipData.isSpawn = !shipData.isSpawn;
        public override void Upgrade() { }
    }
}