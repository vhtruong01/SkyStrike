namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyMoveDataObserver : IEnemyActionDataObserver
        {
            public DataObserver<float> dirX { get;private set; }
            public DataObserver<float> dirY { get; private set; }
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> scale { get; private set; }
            public DataObserver<float> delay { get; private set; }
            public DataObserver<bool> isSyncRotation { get; private set; }
            public int index { get; set; }

            public EnemyMoveDataObserver()
            {
                dirX = new();
                dirY = new();
                rotation = new();
                scale = new();
                isSyncRotation = new();
            }
        }
    }
}