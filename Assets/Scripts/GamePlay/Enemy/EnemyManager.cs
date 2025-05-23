namespace SkyStrike.Game
{
    public class EnemyManager : ObjectManager<Enemy,EnemyMetaData>
    {
        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<EnemyEventData>(CreateObject);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<EnemyEventData>(CreateObject);
        }
        protected override void DestroyItem(Enemy enemy)
        {
            base.DestroyItem(enemy);
            if (pool.CountActive == 0)
                EventManager.Active(EEventType.PlayNextWave);
        }
    }
}