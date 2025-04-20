using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Data/Bullet")]
    public class ShipBulletMetaData : ScriptableObject, IGame
    {
        [field: SerializeField] public int maxLevel { get; private set; }
        [field: SerializeField] public int dmg { get; private set; }
        [field: SerializeField] public float speed { get; private set; }
        [field: SerializeField] public float timeCooldown { get; private set; }
        [field: SerializeField] public float timeLife { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Material material { get; private set; }
        [field: SerializeField] public EShipBulletType type { get; private set; }
    }
}