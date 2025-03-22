using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveDataObserver : IDataList<ObjectDataObserver>, IEditorData<WaveData, WaveDataObserver>
        {
            private static readonly int min = 10000;
            private static readonly int max = 100000;
            private readonly List<ObjectDataObserver> objectDataList;
            private readonly Dictionary<int, ObjectDataObserver> objectDict;
            public DataObserver<float> delay { get; private set; }
            public DataObserver<string> name { get; private set; }
            public DataObserver<bool> isBoss { get; private set; }

            public WaveDataObserver()
            {
                objectDataList = new();
                objectDict = new();
                delay = new();
                name = new();
                isBoss = new();
            }
            public List<ObjectDataObserver> GetList() => objectDataList;
            public ObjectDataObserver CreateEmpty() => null;
            public void Add(ObjectDataObserver data)
            {
                objectDataList.Add(data);
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
                objectDataList.Remove(data);
                objectDict.Remove(data.id);
            }
            public void Remove(int index)
            {
                var data = objectDataList[index];
                data.UnbindAll();
                objectDataList.RemoveAt(index);
                objectDict.Remove(data.id);
            }
            public void Swap(int i1,int i2) => objectDataList.Swap(i1,i2);
            public void Clear() => objectDataList.Clear();
            public WaveDataObserver Clone()
            {
                WaveDataObserver newWave = new();
                foreach (var objectData in objectDataList)
                    newWave.Add(objectData.Clone());
                newWave.delay.SetData(delay.data);
                newWave.isBoss.SetData(isBoss.data);
                return newWave;
            }
            public WaveData ToGameData()
            {
                WaveData waveData = new();
                waveData.delay = delay.data;
                waveData.objectDataArr = new ObjectData[objectDataList.Count];
                for (int i = 0; i < objectDataList.Count; i++)
                    waveData.objectDataArr[i] = objectDataList[i].ToGameData();
                return waveData;
            }
        }
    }
}