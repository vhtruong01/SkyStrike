using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class LevelData : IGame
        {
            public int curBulletId;
            public int starRating;
            public string name;
            public WaveData[] waves;
            public EnemyBulletData[] bullets;
        }
    }
}