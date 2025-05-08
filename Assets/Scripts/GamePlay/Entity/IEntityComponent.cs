namespace SkyStrike.Game
{
    public interface IEntityComponent : IInitalizable
    {
        public IObject entity { get; set; }
        public void Interrupt();
    }
}