using UnityEngine.Events;

namespace SkyStrike
{
    namespace Game
    {
        public static class EventManager
        {
            public static UnityEvent<Enemy> onRemoveEnemy { get; private set; }
            public static UnityEvent<Item> onRemoveItem { get; private set; }

            static EventManager()
            {
                onRemoveEnemy = new();
                onRemoveItem = new();
            }
            public static void RemoveEnemy(Enemy enemy) => onRemoveEnemy.Invoke(enemy);
            public static void RemoveItem(Item item) => onRemoveItem.Invoke(item);
        }
    }
}