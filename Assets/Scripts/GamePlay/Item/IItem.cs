namespace SkyStrike
{
    namespace Game
    {
        public interface IItem : IObject
        {
            public EItem type { get; }
            public int quantity { get; }
        }
    }
}