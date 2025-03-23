using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class LevelData : IGameData
        {
            public string name;
            public WaveData[] waves;
        }
    }
}