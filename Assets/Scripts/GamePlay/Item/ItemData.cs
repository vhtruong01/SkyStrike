using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
        public class ItemData : ScriptableObject
        {
            public string itemName;
            public EItem type;
            public EItemAnimationType animationType;
            public Sprite sprite;
            public Vector2 velocity;
            public int quantity;
        }
    }
}