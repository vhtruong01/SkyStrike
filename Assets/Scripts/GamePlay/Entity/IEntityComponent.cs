namespace SkyStrike.Game
{
    public interface IEntityComponent : IInitalizable
    {
        public IEntity entity { get; set; }
        public void Interrupt();
    }
}