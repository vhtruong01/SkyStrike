using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "MetaData", menuName = "Data/MetaData")]
        public class MetaData : ScriptableObject, IGameData
        {
            [field: SerializeField] public int id { get; set; }
            [field: SerializeField] public Sprite sprite { get; set; }
            [field: SerializeField] public Color color { get; set; }
            [field: SerializeField] public string type { get; set; }
        }
    }
}