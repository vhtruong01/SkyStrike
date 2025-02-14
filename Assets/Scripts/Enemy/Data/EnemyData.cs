using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class EnemyData : IEnemyData
        {
            public string type { get; set; }
            public float rotation { get; set; }
            public Vector2 position { get; set; }
            public Vector2 scale { get; set; }
            public PhaseData phase { get; set; }
            public Sprite sprite { get; set; }

            public EnemyData() { }
            public EnemyData(IEnemyData metaData)
            {
                type = metaData.type;
                position = metaData.position;
                rotation = metaData.rotation;
                scale = metaData.scale;
                sprite = metaData.sprite;
                phase = metaData.phase?.Clone();
                phase ??= new();
            }
            public IEnemyData Clone() => new EnemyData(this);
        }
    }
}