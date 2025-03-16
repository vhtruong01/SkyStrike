namespace SkyStrike
{
    namespace Editor
    {
        public class FireDataObserver : ICloneable<FireDataObserver>
        {
            public FireDataObserver Clone()
            {
                FireDataObserver newAction = new();
                return newAction;
            }
        }
    }
}