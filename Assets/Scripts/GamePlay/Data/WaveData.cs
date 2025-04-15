using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class WaveData : IGame
        {
            public float delay;
            public string name;
            public bool isBoss;
            public float duration;
            public ObjectData[] objectDataArr;
        }
    }
}