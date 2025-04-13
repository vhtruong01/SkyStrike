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
            public WaveDataObserver(WaveData waveData) : this() => ImportData(waveData);
            public void GetList(out List<ObjectDataObserver> list) => list = objectDataList;
            public void CreateEmpty(out ObjectDataObserver data) => data = null;
            public void Add(ObjectDataObserver data)
            {
                objectDataList.Add(data);
                if (data.id.data == ObjectDataObserver.NULL_OBJECT_ID)
                {
                    int id;
                    int cnt = 0;
                    do
                    {
                        id = Random.Range(min, max);
                        ++cnt;
                    }
                    while (objectDict.ContainsKey(id) && cnt < 10000);
                    data.id.SetData(id);
                }
                objectDict.Add(data.id.data, data);
            }
            public void Remove(ObjectDataObserver data)
            {
                data.UnbindAll();
                objectDataList.Remove(data);
                objectDict.Remove(data.id.data);
            }
            public void Remove(int index, out ObjectDataObserver data)
            {
                data = objectDataList[index];
                data.UnbindAll();
                objectDataList.RemoveAt(index);
                objectDict.Remove(data.id.data);
            }
            public WaveDataObserver Clone()
            {
                WaveDataObserver newWave = new();
                foreach (var objectData in objectDataList)
                {
                    var newObject = objectData.Clone();
                    newObject.id = objectData.id;
                    newWave.Add(newObject);
                }
                for (int i = 0; i < newWave.objectDataList.Count; i++)
                    newWave.objectDataList[i].SetRefData(newWave.objectDict.GetValueOrDefault(objectDataList[i].refId));
                newWave.delay.OnlySetData(delay.data);
                newWave.isBoss.OnlySetData(isBoss.data);
                return newWave;
            }
            public WaveData ExportData()
            {
                WaveData waveData = new()
                {
                    delay = delay.data,
                    isBoss = isBoss.data,
                    name = name.data,
                    objectDataArr = new ObjectData[objectDataList.Count]
                };
                for (int i = 0; i < objectDataList.Count; i++)
                    waveData.objectDataArr[i] = objectDataList[i].ExportData();
                return waveData;
            }
            public void ImportData(WaveData waveData)
            {
                if (waveData == null) return;
                delay.OnlySetData(waveData.delay);
                isBoss.OnlySetData(waveData.isBoss);
                name.OnlySetData(waveData.name);
                if (waveData.objectDataArr != null)
                    for (int i = 0; i < waveData.objectDataArr.Length; i++)
                        Add(new(waveData.objectDataArr[i]));
                RefreshRefObject();
            }
            private void RefreshRefObject()
            {
                for (int i = 0; i < objectDataList.Count; i++)
                    objectDataList[i].SetRefData(objectDict.GetValueOrDefault(objectDataList[i].refId));
            }
            public void Set(int index, ObjectDataObserver data)
                => objectDict[index] = data;
        }
    }
}