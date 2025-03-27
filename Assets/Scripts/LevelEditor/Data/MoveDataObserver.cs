using SkyStrike.Game;
using System.Collections.Generic;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveDataObserver : ActionDataObserver, IDataList<PointDataObserver>, IEditorData<MoveData, ActionDataObserver>
        {
            private List<PointDataObserver> points;
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> scale { get; private set; }
            public DataObserver<float> accleration { get; private set; }
            public DataObserver<bool> isSyncRotation { get; private set; }

            public MoveDataObserver() : base()
            {
                points = new();
                rotation = new();
                scale = new();
                accleration = new();
                isSyncRotation = new();
                scale.SetData(1);
            }
            public MoveDataObserver(MoveData moveData) : this() => ImportData(moveData);
            public override ActionDataObserver Clone()
            {
                MoveDataObserver newAction = new();
                newAction.rotation.SetData(rotation.data);
                newAction.scale.SetData(scale.data);
                newAction.delay.SetData(delay.data);
                newAction.isLoop.SetData(isLoop.data);
                newAction.accleration.SetData(accleration.data);
                newAction.isSyncRotation.SetData(isSyncRotation.data);
                for (int i = 0; i < points.Count; i++)
                    newAction.points.Add(points[i].Clone());
                return newAction;
            }
            public MoveData ExportData()
            {
                MoveData data = new()
                {
                    isSyncRotation = isSyncRotation.data,
                    rotation = rotation.data,
                    delay = delay.data,
                    scale = scale.data,
                    isLoop = isLoop.data,
                    accleration = accleration.data,
                    points = new Point[points.Count]
                };
                for (int i = 0; i < points.Count; i++)
                    data.points[i] = points[i].ExportData();
                return data;
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
                //for (int i = 0; i < moveData.points.Length; i++)
                //    Add(new(moveData.points[i]));
            }
            public List<PointDataObserver> GetList() => points;
            public PointDataObserver CreateEmpty()
            {
                PointDataObserver newPoint = new();
                Add(newPoint);
                return newPoint;
            }
            public void Add(PointDataObserver data) => points.Add(data);
            //
            public void Remove(PointDataObserver data)
            {
                throw new System.NotImplementedException();
            }
            public void Remove(int index)
            {
                throw new System.NotImplementedException();
            }
            public void Swap(int leftIndex, int rightIndex) => points.Swap(leftIndex, rightIndex);
            public void Set(int index, PointDataObserver data) => points[index] = data;
        }
    }
}