using UnityEngine;

namespace SkyStrike.Game
{
    public enum EItem
    {
        None = 0,
        Health,
        Star1,
        Star5,
        Energy,
        SingleBullet,
        DoubleBullet,
        TrippleBullet,
        MissileBullet,
        Laser,
        Shield,
        MegaBomb,
    }
    public enum EItemAnimationType
    {
        None = 0,
        Zoom,
        Rotate,
        Fade,
    }
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class ItemMetaData : ScriptableObject, IMetaData, IGame
    {
        [field: SerializeField] public float size { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Material material { get; private set; }
        [field: SerializeField] public EItem type { get; private set; }
        [field: SerializeField] public EItemAnimationType animationType { get; private set; }
    }
}