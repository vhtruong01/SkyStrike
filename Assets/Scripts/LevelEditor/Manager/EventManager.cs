using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public static class EventManager
        {
            public static UnityEvent<IEditorData> onSelectObject { get; private set; }
            public static UnityEvent<IEditorData> onRemoveObject { get; private set; }
            public static UnityEvent<IEditorData> onSetRefObject { get; private set; }
            public static UnityEvent<IEditorData> onSelectMetaObject { get; private set; }
            public static UnityEvent<IEditorData> onCreateObject { get; private set; }
            public static UnityEvent<IEditorData> onSelectWave { get; private set; }
            public static UnityEvent<IEditorData> onSelectLevel { get; private set; }
            public static FuncEvent<IEditorData> onGetLevel { get; private set; }
            public static UnityEvent onPlay { get; private set; }

            static EventManager()
            {
                onSelectObject = new();
                onSetRefObject = new();
                onRemoveObject = new();
                onSelectMetaObject = new();
                onCreateObject = new();
                onSelectWave = new();
                onSelectLevel = new();
                onGetLevel = new();
                onPlay = new();
            }
            public static void SelectObject(IEditorData data) => onSelectObject.Invoke(data);
            public static void RemoveObject(IEditorData data) => onRemoveObject.Invoke(data);
            public static void SetRefObject(IEditorData data) => onSetRefObject.Invoke(data);
            public static void SelectMetaObject(IEditorData data)
            {
                onSelectObject.Invoke(null);
                onSelectMetaObject.Invoke(data);
            }
            public static void CreateObject(IEditorData data) => onCreateObject.Invoke(data);
            public static void SelectWave(IEditorData data) => onSelectWave.Invoke(data);
            public static void SelectLevel(IEditorData data) => onSelectLevel.Invoke(data);
            public static IEditorData GetLevel() => onGetLevel.Invoke();
            public static void Play() => onPlay.Invoke();
        }
    }
}