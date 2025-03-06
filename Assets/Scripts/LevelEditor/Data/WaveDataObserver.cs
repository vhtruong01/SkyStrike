using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver : ICloneable<WaveDataObserver>
        {
            public DataObserver<float> delay { get; set; }
            public List<EnemyDataObserver> enemies { get; private set; }

            public WaveDataObserver()
            {
                enemies = new();
                delay = new();
            }
            public void AddEnemy(EnemyDataObserver enemy) => enemies.Add(enemy);
            public void RemoveEnemy(EnemyDataObserver enemy) => enemies.Remove(enemy);
            public WaveDataObserver Clone()
            {
                WaveDataObserver newWave = new();
                foreach (var enemy in enemies) 
                    newWave.AddEnemy(enemy.Clone());
                //
                return newWave;
            }
        }
    }
}