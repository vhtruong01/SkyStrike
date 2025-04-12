using SkyStrike.Game;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class LevelDataObserver : IDataList<WaveDataObserver>, IEditorData<LevelData, LevelDataObserver>
        {
            private List<WaveDataObserver> waveList;
            public DataObserver<int> star { get; private set; }
            public DataObserver<string> levelName { get; private set; }

            public LevelDataObserver() : this(null) { }
            public LevelDataObserver(LevelData levelData)
            {
                star = new();
                levelName = new();
                ImportData(levelData);
            }
            public List<WaveDataObserver> GetList() => waveList;
            public WaveDataObserver CreateEmpty()
            {
                WaveDataObserver newWave = new();
                Add(newWave);
                return newWave;
            }
            public void Add(WaveDataObserver data) => waveList.Add(data);
            public void Remove(WaveDataObserver data) => waveList.Remove(data);
            public void Remove(int index) => waveList.RemoveAt(index);
            public void Swap(int leftIndex, int rightIndex) => waveList.Swap(leftIndex, rightIndex);
            public void Set(int index, WaveDataObserver data) => waveList[index] = data;
            public LevelData ExportData()
            {
                LevelData levelData = new()
                {
                    name = levelName.data,
                    star = star.data,
                    waves = new WaveData[waveList.Count]
                };
                for (int i = 0; i < waveList.Count; i++)
                    levelData.waves[i] = waveList[i].ExportData();
                return levelData;
            }
            public void ImportData(LevelData levelData)
            {
                waveList = new();
                if (levelData == null)
                {
                    CreateEmpty();
                    return;
                }
                levelName.SetData(levelData.name);
                star.SetData(levelData.star);
                for (int i = 0; i < levelData.waves.Length; i++)
                    Add(new(levelData.waves[i]));
            }
            public LevelDataObserver Clone() => null;
        }
    }
}