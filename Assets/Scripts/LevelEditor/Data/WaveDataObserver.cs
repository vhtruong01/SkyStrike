using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver : ICloneable<WaveDataObserver>
        {
            public DataObserver<float> delay { get; set; }
            public List<ObjectDataObserver> objectList { get; private set; }

            public WaveDataObserver()
            {
                objectList = new();
                delay = new();
            }
            public void AddObject(ObjectDataObserver obj) => objectList.Add(obj);
            public void RemoveObject(ObjectDataObserver obj) => objectList.Remove(obj);
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