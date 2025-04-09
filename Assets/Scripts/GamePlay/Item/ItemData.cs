using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
        public class ItemData : ScriptableObject
        {
            public static readonly float lifeTime = 20;
            public static readonly Vector3 dropVelocity = new(0, -0.5f, 0);
            public string itemName;
            public float size;
            public Sprite sprite;
            public EItem type;
            public EItemAnimationType animationType;
            public Vector2 velocity;
        }
    }
}