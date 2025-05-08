namespace SkyStrike.Game
{
    public class EnemyManager : ObjectManager<Enemy,EnemyMetaData>
    {
        protected override void Subcribe()
            => EventManager.Subscribe<EnemyEventData>(CreateObject);
        protected override void Unsubcribe()
            => EventManager.Unsubscribe<EnemyEventData>(CreateObject);
        protected override void DestroyItem(Enemy enemy)
        {
            base.DestroyItem(enemy);
            if (pool.CountActive == 0)
                EventManager.Active(EEventType.PlayNextWave);
        }
    }
}