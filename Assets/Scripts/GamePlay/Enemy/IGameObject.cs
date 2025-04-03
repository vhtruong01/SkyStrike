namespace SkyStrike
{
    namespace Game
    {
        public interface IGameObject<T> : IObject where T : class
        {
            public void SetData(T data);
        }
    }
}