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
            public static UnityEvent<IData> onSelectObject { get; private set; }
            public static UnityEvent<IData> onSelectMetaObject { get; private set; }
            public static UnityEvent<IData> onCreateObject { get; private set; }
            public static UnityEvent<IData> onSelectWave { get; private set; }
            public static UnityEvent<IData> onSelectLevel { get; private set; }
            public static FuncEvent<IData> onGetLevel { get; private set; }

            static MenuManager()
            {
                onSelectObject = new();
                onSelectMetaObject = new();
                onCreateObject = new();
                onSelectWave = new();
                onSelectLevel = new();
                onGetLevel = new();
            }
            public static void SelectObject(IData data) => onSelectObject.Invoke(data);
            public static void SelectMetaObject(IData data) => onSelectMetaObject.Invoke(data);
            public static void CreateObject(IData data) => onCreateObject.Invoke(data);
            public static void SelectWave(IData data) => onSelectWave.Invoke(data);
            public static void SelectLevel(IData data) => onSelectLevel.Invoke(data);
            public static IData GetLevel() => onGetLevel.Invoke();
        }
    }
}