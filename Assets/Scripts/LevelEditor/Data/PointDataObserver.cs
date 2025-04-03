using static SkyStrike.Game.MoveData;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class PointDataObserver : IEditorData<Point, PointDataObserver>
        {
            public DataObserver<Vector2> prePos { get; private set; }
            public DataObserver<Vector2> midPos { get; private set; }
            public DataObserver<Vector2> nextPos { get; private set; }
            public DataObserver<float> scale { get; private set; }
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> accleration { get; private set; }
            public DataObserver<float> standingTime { get; private set; }
            public DataObserver<float> travelTime { get; private set; }
            public DataObserver<bool> isStraightLine { get; private set; }
            public DataObserver<bool> isLookAtPlayer { get; private set; }
            public DataObserver<bool> isImmortal { get; private set; }
            public DataObserver<bool> isFixedRotation { get; private set; }

            public PointDataObserver()
            {
                prePos = new();
                midPos = new();
                nextPos = new();
                scale = new();
                rotation = new();
                travelTime = new();
                accleration = new();
                standingTime = new();
                isStraightLine = new();
                isLookAtPlayer = new();
                isImmortal = new();
                isFixedRotation = new();
                scale.OnlySetData(1);
                prePos.OnlySetData(new(-1.5f, 0));
                nextPos.OnlySetData(new(0, -1.5f));
            }
            public PointDataObserver(Point data) : this() => ImportData(data);
            public void Translate(Vector2 pos)
            {
                Vector2 dir = pos - midPos.data;
                midPos.SetData(pos);
                prePos.SetData(prePos.data + dir);
                nextPos.SetData(nextPos.data + dir);
            }
            public void UnbindAll()
            {
                prePos.UnbindAll();
                midPos.UnbindAll();
                nextPos.UnbindAll();
                scale.UnbindAll();
                rotation.UnbindAll();
                travelTime.UnbindAll(); 
                accleration.UnbindAll();
                standingTime.UnbindAll();
                isImmortal.UnbindAll();
                isStraightLine.UnbindAll();
                isLookAtPlayer.UnbindAll();
                isFixedRotation.UnbindAll();
            }
            public PointDataObserver Clone()
            {
                PointDataObserver newPoint = new();
                newPoint.prePos.OnlySetData(prePos.data);
                newPoint.nextPos.OnlySetData(nextPos.data);
                newPoint.midPos.OnlySetData(midPos.data);
                newPoint.scale.OnlySetData(scale.data);
                newPoint.rotation.OnlySetData(rotation.data);
                newPoint.travelTime.OnlySetData(travelTime.data);
                newPoint.accleration.OnlySetData(accleration.data);
                newPoint.standingTime.OnlySetData(standingTime.data);
                newPoint.isImmortal.OnlySetData(isImmortal.data);
                newPoint.isLookAtPlayer.OnlySetData(isLookAtPlayer.data);
                newPoint.isStraightLine.OnlySetData(isStraightLine.data);
                newPoint.isFixedRotation.OnlySetData(isFixedRotation.data);
                return newPoint;
            }
            public Point ExportData()
            {
                return new()
                {
                    prevPos = new(prePos.data),
                    midPos = new(midPos.data),
                    nextPos = new(nextPos.data),
                    isStraightLine = isStraightLine.data,
                    isImmortal = isImmortal.data,
                    isLookAtPlayer = isLookAtPlayer.data,
                    isFixedRotation = isFixedRotation.data,
                    scale = scale.data,
                    rotation = rotation.data,
                    accleration = accleration.data,
                    standingTime = standingTime.data,
                    travelTime = travelTime.data,
                };
            }
            public void ImportData(Point pointData)
            {
                prePos.OnlySetData(pointData.prevPos.ToVector2());
                midPos.OnlySetData(pointData.midPos.ToVector2());
                nextPos.OnlySetData(pointData.nextPos.ToVector2());
                isStraightLine.OnlySetData(pointData.isStraightLine);
                isImmortal.OnlySetData(pointData.isImmortal);
                isLookAtPlayer.OnlySetData(pointData.isLookAtPlayer);
                isFixedRotation.OnlySetData(pointData.isFixedRotation);
                scale.OnlySetData(pointData.scale);
                rotation.OnlySetData(pointData.rotation);
                accleration.OnlySetData(pointData.accleration);
                standingTime.OnlySetData(pointData.standingTime);
                travelTime.OnlySetData(pointData.travelTime);
            }
        }
    }
}