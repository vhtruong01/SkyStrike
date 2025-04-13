using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class LevelData : IGame
        {
            public int star;
            public string name;
            public WaveData[] waves;
            public BulletData[] bullets;
        }
    }
}