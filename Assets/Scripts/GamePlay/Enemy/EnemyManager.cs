namespace SkyStrike.Game
{
    public class EnemyManager : PoolManager<Enemy, EnemyData>
    {
        private void OnEnable()
            => EventManager.Subscribe<EnemyData.EnemyEventData>(CreateEnemy);
        private void OnDisable()
            => EventManager.Unsubscribe<EnemyData.EnemyEventData>(CreateEnemy);
        private void CreateEnemy(EnemyData.EnemyEventData eventData)
        {
            Enemy enemy = InstantiateItem(eventData.position);
            enemy.data.SetData(eventData);
            enemy.Strike(eventData.delay);
        }
        protected override void DestroyItem(Enemy enemy)
        {
            base.DestroyItem(enemy);
            if (pool.CountActive == 0)
                EventManager.Active(EEventType.PlayNextWave);
        }
    }
}