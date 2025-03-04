using System;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public static class MenuManager
        {
            public class FuncEvent<T>
            {
                private Func<T> func;
                public void AddListener(Func<T> f) => func = f;
                public void RemoveAllListeners() => func = null;
                public T Invoke() => func.Invoke();
            }
            public static UnityEvent<IData> onSelectEnemy { get; private set; }
            public static UnityEvent<IData> onCreateEnemy { get; private set; }
            public static UnityEvent<IData> onSelectItemUI { get; private set; }
            public static UnityEvent<IData> onRemoveWave { get; private set; }
            public static UnityEvent<IData> onSelectWave { get; private set; }
            public static FuncEvent<IData> onCreateWave { get; private set; }

            static MenuManager()
            {
                onSelectEnemy = new();
                onCreateEnemy = new();
                onSelectItemUI = new();
                onRemoveWave = new();
                onCreateWave = new();
                onSelectWave = new();
            }

            public static void SelectEnemy(IData data) => onSelectEnemy.Invoke(data);
            public static void CreateEnemy(IData data) => onCreateEnemy.Invoke(data);
            public static void SelectItemUI(IData data) => onSelectItemUI.Invoke(data);
            public static void RemoveWave(IData data) => onRemoveWave.Invoke(data);
            public static void SelectWave(IData data) => onSelectWave.Invoke(data);
            public static IData CreateWave() => onCreateWave.Invoke();
        }
    }
}