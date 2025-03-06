using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class LevelDataObserver : IDataList<WaveDataObserver>
        {
            private string name;
            private int star;
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
        }
    }
}