namespace SkyStrike.Game
{
    public interface IShipComponent : IEntityComponent
    {
        public ShipData data { get; set; }
    }
}