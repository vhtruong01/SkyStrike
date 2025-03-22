namespace SkyStrike
{
    namespace Editor
    {
        public interface IEditorData<T, N> : ICloneable<N> where T : class where N : class
        {
            public T ToGameData();
        }
    }
}