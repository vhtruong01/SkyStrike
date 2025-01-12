using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class EnemyData : IEnemyData
        {
            public string type { get; set; }
            public Vector2 position { get; set; }
            public Vector2 rotation { get; set; }
            public Vector2 scale { get; set; }
            public Sprite sprite { get; set; }
        }
    }
}