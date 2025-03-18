using SkyStrike.Game;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class LevelDataObserver : IDataList<WaveDataObserver>
        {
            public string name;
            public int star;
            private List<WaveDataObserver> waveList;

            public LevelDataObserver() => waveList = new();
            public List<WaveDataObserver> GetList() => waveList;
            public WaveDataObserver Create()
            {
                WaveDataObserver newWave = new();
                waveList.Add(newWave);
                return newWave;
            }
            public void Remove(WaveDataObserver data) => waveList.Remove(data);
            public void Swap(int leftIndex, int rightIndex)
            {
                if (leftIndex > 0 & rightIndex < waveList.Count)
                    (waveList[leftIndex], waveList[rightIndex]) = (waveList[leftIndex], waveList[rightIndex]);
            }
            public IGameData ToGameData()
            {
                LevelData levelData = new();
                levelData.waves = new WaveData[waveList.Count];
                for (int i = 0; i < waveList.Count; i++)
                    levelData.waves[i] = waveList[i].ToGameData() as WaveData;
                return levelData;
            }
        }
    }
}