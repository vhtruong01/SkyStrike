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
            public MoveDataObserver(MoveData moveData) : this() => ImportData(moveData);
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
            public MoveData ExportData()
            {
                return new()
                {
                    isSyncRotation = isSyncRotation.data,
                    rotation = rotation.data,
                    delay = delay.data,
                    scale = scale.data,
                    isLoop = isLoop.data,
                    accleration = accleration.data,
                    radius = radius.data,
                    dir = new(dirX.data, dirY.data)
                };
            }
            public void ImportData(MoveData moveData)
            {
                if (moveData == null) return;
                isSyncRotation.SetData(moveData.isSyncRotation);
                rotation.SetData(moveData.rotation);
                delay.SetData(moveData.delay);
                scale.SetData(moveData.scale);
                isLoop.SetData(moveData.isLoop);
                accleration.SetData(moveData.accleration);
                radius.SetData(moveData.radius);
                dirX.SetData(moveData.dir.x);
                dirY.SetData(moveData.dir.y);
            }
        }
    }
}