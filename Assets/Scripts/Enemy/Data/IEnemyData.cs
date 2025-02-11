using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public interface IEnemyData : IData
        {
            public string type { get; set; }
            public float rotation { get; set; }
            public Vector2 position { get; set; }
            public Vector2 scale { get; set; }
            public Sprite sprite { get; set; }
            public PhaseData phase { get; set; }
            public IEnemyData Clone();
        }
    }
}