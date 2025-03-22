using SkyStrike.Game;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        [CreateAssetMenu(fileName = "MetaData", menuName = "Data/ObjectMetaData")]
        public class ObjectMetaData : MetaData
        {
            [field: SerializeField] public string type { get; set; }
            [field: SerializeField] public float rotation { get; set; }
            [field: SerializeField] public Vector2 position { get; set; }
            [field: SerializeField] public Vector2 scale { get; set; }
            [field: SerializeField] public Vector2 velocity { get; set; }
        }
    }
}