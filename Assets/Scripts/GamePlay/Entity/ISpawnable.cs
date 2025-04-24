namespace SkyStrike.Game
{
    public interface ISpawnable : IEntityComponent
    {
        public void Spawn();
        public void Stop();
    }
}