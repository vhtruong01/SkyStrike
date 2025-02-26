using SkyStrike.Enemy;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        [CreateAssetMenu(fileName = "MetaData", menuName = "Data/EnemyMetaData")]
        public class EnemyMetaData : ScriptableObject
        {
            public string id;
            [field: SerializeField] public string type { get; set; }
            [field: SerializeField] public float rotation { get; set; }
            [field: SerializeField] public Vector2 position { get; set; }
            [field: SerializeField] public Vector2 scale { get; set; }
            [field: SerializeField] public Vector2 velocity { get; set; }
            [field: SerializeField] public Sprite sprite { get; set; }
        }
    }
}