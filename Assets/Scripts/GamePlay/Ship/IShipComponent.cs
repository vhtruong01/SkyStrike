namespace SkyStrike.Game
{
    public interface IShipComponent : IEntityComponent
    {
        public ShipData shipData { get; set; }
    }
}