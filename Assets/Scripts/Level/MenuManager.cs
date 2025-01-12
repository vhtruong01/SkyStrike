using SkyStrike.Enemy;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public static class MenuManager 
        {
            public static readonly UnityEvent<IEnemyData> onSelectEnemy = new();
            //public static readonly UnityEvent onSelectEnemyGroup = new();

            public static void SelectEnemy(IEnemyData enemyData) => onSelectEnemy.Invoke(enemyData);

        }
    }
}