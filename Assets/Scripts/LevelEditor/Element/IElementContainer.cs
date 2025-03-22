namespace SkyStrike
{
    namespace Editor
    {
        public interface IElementContainer<T> where T : class
        {
            public IDataList<T> GetDataList();
        }
    }
}