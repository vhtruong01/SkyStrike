
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public static class MenuManager
        {
            public static UnityEvent<IData> onSelectEnemy { get; private set; }
            public static UnityEvent<IData> onCreateEnemy { get; private set; }
            public static UnityEvent<IData> onSelectItemUI { get; private set; }
            static MenuManager()
            {
                onSelectEnemy = new();
                onCreateEnemy = new();
                onSelectItemUI = new();
            }

            public static void SelectEnemy(IData data) => onSelectEnemy.Invoke(data);
            public static void CreateEnemy(IData data) => onCreateEnemy.Invoke(data);
            public static void SelectItemUI(IData data) => onSelectItemUI.Invoke(data);
        }
    }
}