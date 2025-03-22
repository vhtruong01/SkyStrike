namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionDataObserver : ICloneable<ActionDataObserver>
        {
            public DataObserver<bool> isLoop { get; protected set; }
            public DataObserver<float> delay { get; protected set; }

            public ActionDataObserver()
            {
                delay = new();
                isLoop = new();
            }
            public abstract ActionDataObserver Clone();
        }
    }
}