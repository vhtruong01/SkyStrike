namespace SkyStrike
{
    namespace Ship
    {
        public interface IBulletSpawner
        {
            public void StartSpawn();
            public void StopSpawn();
            public void Spawn();
        }
    }
}