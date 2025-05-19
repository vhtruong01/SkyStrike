using System;

namespace SkyStrike.Game
{
    [Serializable]
    public class LevelData : IGame
    {
        [NonSerialized] public string fileName;
        public int enemyCount;
        public int curBulletId;
        public int starRating;
        public string name;
        public WaveData[] waves;
        public EnemyBulletMetaData[] bullets;

        public float percentRequired => Math.Min(1, 0.01f * (70 + 5 * starRating));
    }
}