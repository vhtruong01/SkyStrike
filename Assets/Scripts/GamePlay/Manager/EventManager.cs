using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public static class EventManager
    {
        public static UnityEvent<Vector3, int> onDropStar { get; private set; }
        public static UnityEvent<EItem, Vector3> onDropItem { get; private set; }
        public static UnityEvent<EnemyBulletMetaData, Vector3, float> onSpawnEnemyBullet { get; private set; }
        public static UnityEvent<ShipBulletMetaData> onSpawnShipBullet { get; private set; }
        public static UnityEvent<ObjectData, EItem, float> onCreateEnemy { get; private set; }
        public static UnityEvent onPlayNextWave { get; private set; }
        public static FuncEvent<Vector3> onGetShipPosition { get; private set; }

        static EventManager()
        {
            onDropItem = new();
            onDropStar = new();
            onCreateEnemy = new();
            onPlayNextWave = new();
            onGetShipPosition = new();
            onSpawnShipBullet = new();
            onSpawnEnemyBullet = new();
        }
        public static void DropItem(EItem type, Vector3 position)
            => onDropItem.Invoke(type, position);
        public static void DropStar(Vector3 position, int amount)
            => onDropStar.Invoke(position, amount);
        public static void SpawnEnemyBullet(EnemyBulletMetaData metaData, Vector3 position, float angle)
            => onSpawnEnemyBullet.Invoke(metaData, position, angle);
        public static void SpawnShipBullet(ShipBulletMetaData metaData)
            => onSpawnShipBullet.Invoke(metaData);
        public static void CreateEnemy(ObjectData data, EItem itemType, float delay)
            => onCreateEnemy.Invoke(data, itemType, delay);
        public static void PlayNextWave() => onPlayNextWave.Invoke();
    }
}