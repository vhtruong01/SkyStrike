using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class WaveData : IGameData
        {
            public float delay;
            public string name;
            public bool isBoss;
            public ObjectData[] objectDataArr;
        }
    }
}