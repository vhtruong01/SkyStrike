namespace SkyStrike.Game
{
    public interface IEnemyComponent : IEntityComponent
    {
        public EnemyData enemyData { get; set; }
        public void RefreshData();
    }
}