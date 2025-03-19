using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver : ICloneable<WaveDataObserver>, IDataList<ObjectDataObserver>
        {
            private static readonly int min = 10000;
            private static readonly int max = 100000;
            private readonly List<ObjectDataObserver> objectList;
            private readonly Dictionary<int, ObjectDataObserver> objectDict;
            public DataObserver<float> delay { get; private set; }
            public DataObserver<string> name { get; private set; }
            public DataObserver<bool> isBoss { get; private set; }

            public WaveDataObserver()
            {
                objectList = new();
                objectDict = new();
                delay = new();
                name = new();
                isBoss = new();
            }
            public List<ObjectDataObserver> GetList() => objectList;
            public ObjectDataObserver CreateEmpty() => null;
            public void Add(ObjectDataObserver data)
            {
                objectList.Add(data);
                int id;
                int cnt = 0;
                do
                {
                    id = Random.Range(min, max);
                    ++cnt;
                }
                while (objectDict.ContainsKey(id) && cnt < 10000);
                data.id = id;
                objectDict.Add(data.id, data);
            }
            public void Remove(ObjectDataObserver data)
            {
                data.UnbindAll();
                objectList.Remove(data);
                objectDict.Remove(data.id);
            }
            public void Remove(int index)
            {
                var data = objectList[index];
                data.UnbindAll();
                objectList.RemoveAt(index);
                objectDict.Remove(data.id);
            }
            public void Swap(int leftIndex, int rightIndex) => objectList.Swap(leftIndex, rightIndex);
            public WaveDataObserver Clone()
            {
                WaveDataObserver newWave = new();
                foreach (var objectData in objectList)
                    newWave.Add(objectData.Clone());
                newWave.delay.SetData(delay.data);
                newWave.isBoss.SetData(isBoss.data);
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