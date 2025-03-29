using UnityEngine.Events;

namespace SkyStrike
{
    namespace Game
    {
        public static class EventManager
        {
            public static FuncEvent<int, IGameData> onGetMetaData { get; private set; }
            public static UnityEvent<IGameObject> onRemoveObject { get; private set; }
            public static UnityEvent<Item> onRemoveItem { get; private set; }

            static EventManager()
            {
                onGetMetaData = new();
                onRemoveObject = new();
                onRemoveItem = new();
            }
            public static IGameData GetMetaData(int id) => onGetMetaData.Invoke(id);
            public static void RemoveObject(IGameObject gameObject) => onRemoveObject.Invoke(gameObject);
            public static void RemoveItem(Item item) => onRemoveItem.Invoke(item);
        }
    }
}