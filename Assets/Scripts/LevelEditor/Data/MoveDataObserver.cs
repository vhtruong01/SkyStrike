namespace SkyStrike
{
    namespace Editor
    {
        public class MoveDataObserver : ICloneable<MoveDataObserver>
        {
            public DataObserver<float> dirX { get; private set; }
            public DataObserver<float> dirY { get; private set; }
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> scale { get; private set; }
            public DataObserver<float> delay { get; private set; }
            public DataObserver<bool> isSyncRotation { get; private set; }

            public MoveDataObserver()
            {
                dirX = new();
                dirY = new();
                rotation = new();
                scale = new();
                delay = new();
                isSyncRotation = new();
            }

            public MoveDataObserver Clone()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}