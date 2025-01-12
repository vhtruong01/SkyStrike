using SkyStrike.Enemy;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveData
        {
            public float delay {  get; set; }
            public List<EnemyData> enemies {  get; private set; }
            //
        }
    }
}