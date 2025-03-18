using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver : ICloneable<WaveDataObserver>
        {
            private static readonly int min = 10000;
            private static readonly int max = 100000;
            private Dictionary<int, ObjectDataObserver> objectDict;
            public DataObserver<float> delay { get; set; }
            public List<ObjectDataObserver> objectList { get; private set; }

            public WaveDataObserver()
            {
                objectList = new();
                objectDict = new();
                delay = new();
            }
            public void AddObject(ObjectDataObserver objectData)
            {
                objectList.Add(objectData);
                int id;
                int cnt = 0;
                do
                {
                    id = Random.Range(min, max);
                    ++cnt;
                }
                while (objectDict.ContainsKey(id) && cnt < 10000);
                objectData.id = id;
                objectDict.Add(objectData.id, objectData);
            }
            public void RemoveObject(ObjectDataObserver objectData)
            {
                objectData.UnbindAll();
                objectList.Remove(objectData);
                objectDict.Remove(objectData.id);
            }
            public ObjectDataObserver GetObjectDataById(int id)
            {
                return objectDict.TryGetValue(id, out ObjectDataObserver objectDataObserver) ? objectDataObserver : null;
            }
            public WaveDataObserver Clone()
            {
                WaveDataObserver newWave = new();
                foreach (var objectData in objectList)
                    newWave.AddObject(objectData.Clone());
                //
                return newWave;
            }

            public IGameData ToGameData()
            {
                WaveData waveData = new();
                waveData.delay = delay.data;
                waveData.objectDataArr = new ObjectData[objectList.Count];
                for (int i = 0; i < objectList.Count; i++)
                    waveData.objectDataArr[i] = objectList[i].ToGameData() as ObjectData;
                return waveData;
            }
        }
    }
}