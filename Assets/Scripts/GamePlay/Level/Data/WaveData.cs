using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class WaveData : IGameData
        {
            public float delay;
            public ObjectData[] objectDataArr;
        }
    }
}