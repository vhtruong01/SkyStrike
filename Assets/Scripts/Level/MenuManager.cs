using SkyStrike.Enemy;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public static class MenuManager
        {
            public static UnityEvent<IEnemyData> onSelectEnemy { get; private set; }
            public static UnityEvent<IEnemyData> onCreateEnemy { get; private set; }
            static MenuManager()
            {
                onSelectEnemy = new();
                onCreateEnemy = new();
            }

            public static void SelectEnemy(IEnemyData enemyData) => onSelectEnemy.Invoke(enemyData);
            public static void CreateEnemy(IEnemyData enemyData) => onCreateEnemy.Invoke(enemyData);

        }
    }
}