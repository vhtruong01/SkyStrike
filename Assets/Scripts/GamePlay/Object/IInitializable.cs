namespace SkyStrike.Game
{
    public interface IInitializable<T>
    {
        public void SetData(T data);
    }
}