using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "EnemyMetaData", menuName = "Data/EnemyMetaData")]
    public class EnemyMetaData : ScriptableObject, IGame
    {
        [field: SerializeField] public int id { get; private set; }
        [field: SerializeField] public int star { get; private set; }
        [field: SerializeField] public int maxHp { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Color color { get; private set; }
        [field: SerializeField] public string type { get; private set; }
    }
}