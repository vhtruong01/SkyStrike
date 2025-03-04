using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class LevelDataObserver : IData
        {
            private string name;
            private int star;
            private List<WaveDataObserver> waveList;

            public LevelDataObserver()
            {
                waveList = new();
            }

            public WaveDataObserver CreateWave()
            {
                WaveDataObserver newWave = new();
                waveList.Add(newWave);
                return newWave;
            }
            public void RemoveWave(IData data)
            {
                if (data is WaveDataObserver wave)
                    waveList.Remove(wave);
            }
            public WaveDataObserver GetWave(int index)
            {
                return waveList[index];
            }
        }
    }
}