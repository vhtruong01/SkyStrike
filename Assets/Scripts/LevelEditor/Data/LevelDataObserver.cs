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

            public WaveDataObserver CreateNewWave()
            {
                WaveDataObserver newWave = new();
                waveList.Add(newWave);
                return newWave;
            }
            public void DeleteWave(int index)
            {

            }
            public WaveDataObserver GetWave(int index)
            {
                return waveList[index];
            }
        }
    }
}