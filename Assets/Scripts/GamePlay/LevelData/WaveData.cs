using System;

namespace SkyStrike.Game
{
    [Serializable]
    public class WaveData : IGame
    {
        public string name;
        public bool isBoss;
        public float delay;
        public float duration;
        public ObjectData[] objectDataArr;
    }
}