namespace SkyStrike
{
    namespace Game
    {
        public static class EventManager
        {
            public static FuncEvent<int, IGameData> onGetMetaData { get; private set; }

            static EventManager()
            {
                onGetMetaData = new();
            }
            public static IGameData GetMetaData(int id) => onGetMetaData.Invoke(id);
        }
    }
}