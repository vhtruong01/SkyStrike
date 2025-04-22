using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class ItemData : ScriptableObject, IGame
    {
        public static readonly float lifeTime = 20;
        public static readonly Vector3 dropVelocity = new(0, -0.5f, 0);
        [field: SerializeField] public float size { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Material material { get; private set; }
        [field: SerializeField] public EItem type { get; private set; }
        [field: SerializeField] public EItemAnimationType animationType { get; private set; }
    }
}