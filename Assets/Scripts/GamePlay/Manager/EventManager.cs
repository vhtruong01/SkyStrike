using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Game
    {
        public static class EventManager
        {
            public static UnityEvent<Vector3, int> onDropStar { get; private set; }
            public static UnityEvent<EItem, Vector3> onDropItem { get; private set; }
            public static UnityEvent<EnemyBulletData, Vector3, float> onSpawnEnemyBullet { get; private set; }

            static EventManager()
            {
                onDropItem = new();
                onDropStar = new();
                onSpawnEnemyBullet = new();
            }
            public static void DropItem(EItem type, Vector3 position)
                => onDropItem.Invoke(type, position);
            public static void DropStar(Vector3 position, int amount)
                => onDropStar.Invoke(position, amount);
            public static void SpawnEnemyBullet(EnemyBulletData data, Vector3 position, float angle)
                => onSpawnEnemyBullet.Invoke(data, position, angle);
        }
    }
}