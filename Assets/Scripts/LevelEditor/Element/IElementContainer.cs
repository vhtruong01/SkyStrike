namespace SkyStrike.Editor
{
    public interface IElementContainer<T> where T : IEditor
    {
        public IDataList<T> GetDataList();
    }
}