using System.Collections.Generic;
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
        [field: SerializeField] public List<Sprite> destructionSprites { get; private set; }
        [field: SerializeField] public List<Sprite> engineSprites { get; private set; }
        [field: SerializeField] public List<Sprite> shieldSprites { get; private set; }
        [field: SerializeField] public List<Sprite> weaponSprites { get; private set; }

    }
}