namespace SkyStrike.Editor
{
    public interface ICloneable<T> : IEditor where T : IEditor
    {
        public T Clone();
    }
}