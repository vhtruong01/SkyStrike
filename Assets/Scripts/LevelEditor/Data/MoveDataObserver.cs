using SkyStrike.Game;

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
                scale.SetData(1);
            }
            public MoveDataObserver Clone()
            {
                MoveDataObserver newAction = new();
                newAction.dirX.SetData(dirX.data);
                newAction.dirY.SetData(dirY.data);
                newAction.rotation.SetData(rotation.data);
                newAction.scale.SetData(scale.data);
                newAction.delay.SetData(delay.data);
                newAction.isSyncRotation.SetData(isSyncRotation.data);
                return newAction;
            }
            public IGameData ToGameData()
            {
                MoveData moveData = new();
                moveData.isSyncRotation = isSyncRotation.data;
                moveData.rotation = rotation.data;
                moveData.delay = delay.data;
                moveData.scale = scale.data;
                moveData.dir = new(dirX.data, dirY.data);
                return moveData;
            }
        }
    }
}