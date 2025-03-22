namespace SkyStrike
{
    namespace Editor
    {
        public interface ICloneable<T> where T : class
        {
            public T Clone();
        }
    }
}