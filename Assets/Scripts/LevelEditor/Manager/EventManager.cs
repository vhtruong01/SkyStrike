using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public static class EventManager
        {
            public static UnityEvent<ObjectDataObserver> onSelectObject { get; private set; }
            public static UnityEvent<ObjectDataObserver> onRemoveObject { get; private set; }
            public static UnityEvent<ObjectDataObserver> onSetRefObject { get; private set; }
            public static UnityEvent<ObjectDataObserver> onSelectMetaObject { get; private set; }
            public static UnityEvent<ObjectDataObserver> onCreateObject { get; private set; }
            public static UnityEvent<WaveDataObserver> onSelectWave { get; private set; }
            public static UnityEvent<LevelDataObserver> onSelectLevel { get; private set; }
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
                onPlay = new();
            }
            public static void SelectObject(ObjectDataObserver data) => onSelectObject.Invoke(data);
            public static void SetRefObject(ObjectDataObserver data) => onSetRefObject.Invoke(data);
            public static void RemoveObject(ObjectDataObserver data) => onRemoveObject.Invoke(data);
            public static void SelectMetaObject(ObjectDataObserver data)
            {
                onSelectObject.Invoke(null);
                onSelectMetaObject.Invoke(data);
            }
            public static void CreateObject(ObjectDataObserver data) => onCreateObject.Invoke(data);
            public static void SelectWave(WaveDataObserver data) => onSelectWave.Invoke(data);
            public static void SelectLevel(LevelDataObserver data) => onSelectLevel.Invoke(data);
            public static void Play() => onPlay.Invoke();
        }
    }
}