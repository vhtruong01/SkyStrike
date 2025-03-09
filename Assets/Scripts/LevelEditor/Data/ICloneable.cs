namespace SkyStrike
{
    namespace Editor
    {
        public interface ICloneable<T>: IEditorData
        {
            public T Clone();
        }
    }
}