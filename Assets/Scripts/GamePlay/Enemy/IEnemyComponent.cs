namespace SkyStrike.Game
{
    public interface IEnemyComponent : IEntityComponent
    {
        public EnemyData data { get; set; }
    }
}