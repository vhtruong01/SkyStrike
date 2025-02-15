using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver
        {
            public DataObserver<float> delay { get; set; }
            private HashSet<EnemyDataObserver> enemies;
            public WaveDataObserver()
            {
                enemies = new();
                delay = new();
            }
            public void AddEnemy(EnemyDataObserver enemy) => enemies.Add(enemy);
            public void RemoveEnemy(EnemyDataObserver enemy) => enemies.Remove(enemy);
        }
    }
}