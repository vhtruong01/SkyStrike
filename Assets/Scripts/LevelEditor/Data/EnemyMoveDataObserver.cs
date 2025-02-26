namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyMoveDataObserver : ICloneable<EnemyMoveDataObserver>
        {
            public DataObserver<float> dirX { get; private set; }
            public DataObserver<float> dirY { get; private set; }
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> scale { get; private set; }
            public DataObserver<float> delay { get; private set; }
            public DataObserver<bool> isSyncRotation { get; private set; }

            public EnemyMoveDataObserver()
            {
                dirX = new();
                dirY = new();
                rotation = new();
                scale = new();
                delay = new();
                isSyncRotation = new();
            }

            public EnemyMoveDataObserver Clone()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}