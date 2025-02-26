namespace SkyStrike
{
    namespace Editor
    {
        public interface ICloneable<T>: IData
        {
            public T Clone();
        }
    }
}