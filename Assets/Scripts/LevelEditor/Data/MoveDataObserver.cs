using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveDataObserver : ActionDataObserver, IEditorData<MoveData, ActionDataObserver>
        {
            public DataObserver<float> dirX { get; private set; }
            public DataObserver<float> dirY { get; private set; }
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> scale { get; private set; }
            public DataObserver<float> accleration { get; private set; }
            public DataObserver<float> radius { get; private set; }
            public DataObserver<bool> isSyncRotation { get; private set; }

            public MoveDataObserver() : base()
            {
                dirX = new();
                dirY = new();
                rotation = new();
                scale = new();
                accleration = new();
                radius = new();
                isSyncRotation = new();
                scale.SetData(1);
            }
            public override ActionDataObserver Clone()
            {
                MoveDataObserver newAction = new();
                newAction.dirX.SetData(dirX.data);
                newAction.dirY.SetData(dirY.data);
                newAction.rotation.SetData(rotation.data);
                newAction.scale.SetData(scale.data);
                newAction.delay.SetData(delay.data);
                newAction.isLoop.SetData(isLoop.data);
                newAction.accleration.SetData(accleration.data);
                newAction.radius.SetData(radius.data);
                newAction.isSyncRotation.SetData(isSyncRotation.data);
                return newAction;
            }
            public MoveData ToGameData()
            {
                MoveData moveData = new();
                moveData.isSyncRotation = isSyncRotation.data;
                moveData.rotation = rotation.data;
                moveData.delay = delay.data;
                moveData.scale = scale.data;
                //
                moveData.dir = new(dirX.data, dirY.data);
                return moveData;
            }
        }
    }
}