using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver : ICloneable<WaveDataObserver>
        {
            public DataObserver<float> delay { get; set; }
            public List<EnemyDataObserver> objectList { get; private set; }

            public WaveDataObserver()
            {
                objectList = new();
                delay = new();
            }
            public void AddObject(EnemyDataObserver obj) => objectList.Add(obj);
            public void RemoveObject(EnemyDataObserver obj) => objectList.Remove(obj);
            public WaveDataObserver Clone()
            {
                WaveDataObserver newWave = new();
                foreach (var obj in objectList) 
                    newWave.AddObject(obj.Clone());
                //
                return newWave;
            }
        }
    }
}