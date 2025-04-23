using System.Collections.Generic;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike.Editor
{
    public class PointDataObserver : IEditorData<Point, PointDataObserver>
    {
        public DataObserver<Vector2> prePos { get; private set; }
        public DataObserver<Vector2> midPos { get; private set; }
        public DataObserver<Vector2> nextPos { get; private set; }
        public DataObserver<float> scale { get; private set; }
        public DataObserver<float> rotation { get; private set; }
        public DataObserver<float> standingTime { get; private set; }
        public DataObserver<float> travelTime { get; private set; }
        public DataObserver<bool> shield { get; private set; }
        public DataObserver<bool> isStraightLine { get; private set; }
        public DataObserver<bool> isLookingAtPlayer { get; private set; }
        public DataObserver<bool> isImmortal { get; private set; }
        public DataObserver<bool> isIgnoreVelocity { get; private set; }
        public HashSet<int> bulletIdSet { get; private set; }

        public PointDataObserver()
        {
            prePos = new();
            midPos = new();
            nextPos = new();
            scale = new();
            rotation = new();
            travelTime = new();
            standingTime = new();
            shield = new();
            isStraightLine = new();
            isLookingAtPlayer = new();
            isImmortal = new();
            bulletIdSet = new();
            isIgnoreVelocity = new();
            isStraightLine.SetData(true);
            scale.OnlySetData(1);
            prePos.OnlySetData(new(-1.5f, 0));
            nextPos.OnlySetData(new(0, -1.5f));
        }
        public PointDataObserver(Point data) : this() => ImportData(data);
        public void Translate(Vector2 dir)
        {
            midPos.SetData(midPos.data + dir);
            prePos.SetData(prePos.data + dir);
            nextPos.SetData(nextPos.data + dir);
        }
        public void ChangePosition(Vector2 pos) => Translate(pos - midPos.data);
        public void UnbindAll()
        {
            prePos.UnbindAll();
            midPos.UnbindAll();
            nextPos.UnbindAll();
            scale.UnbindAll();
            rotation.UnbindAll();
            travelTime.UnbindAll();
            standingTime.UnbindAll();
            shield.UnbindAll();
            isImmortal.UnbindAll();
            isStraightLine.UnbindAll();
            isLookingAtPlayer.UnbindAll();
            isIgnoreVelocity.UnbindAll();
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
            newPoint.standingTime.OnlySetData(standingTime.data);
            newPoint.shield.OnlySetData(shield.data);
            newPoint.isImmortal.OnlySetData(isImmortal.data);
            newPoint.isLookingAtPlayer.OnlySetData(isLookingAtPlayer.data);
            newPoint.isStraightLine.OnlySetData(isStraightLine.data);
            newPoint.isIgnoreVelocity.OnlySetData(isIgnoreVelocity.data);
            foreach (int id in bulletIdSet)
                newPoint.bulletIdSet.Add(id);
            return newPoint;
        }
        public Point ExportData()
        {
            Point pointData = new()
            {
                prevPos = new(prePos.data),
                midPos = new(midPos.data),
                nextPos = new(nextPos.data),
                isStraightLine = isStraightLine.data,
                isImmortal = isImmortal.data,
                isLookingAtPlayer = isLookingAtPlayer.data,
                isIgnoreVelocity = isIgnoreVelocity.data,
                shield = shield.data,
                scale = scale.data,
                rotation = rotation.data,
                standingTime = standingTime.data,
                travelTime = travelTime.data,
                bulletIdArr = new int[bulletIdSet.Count],
            };
            int index = 0;
            foreach (int id in bulletIdSet)
                pointData.bulletIdArr[index++] = id;
            return pointData;
        }
        public void ImportData(Point pointData)
        {
            prePos.OnlySetData(pointData.prevPos.ToVector2());
            midPos.OnlySetData(pointData.midPos.ToVector2());
            nextPos.OnlySetData(pointData.nextPos.ToVector2());
            isStraightLine.OnlySetData(pointData.isStraightLine);
            isImmortal.OnlySetData(pointData.isImmortal);
            isLookingAtPlayer.OnlySetData(pointData.isLookingAtPlayer);
            isIgnoreVelocity.OnlySetData(pointData.isIgnoreVelocity);
            shield.OnlySetData(pointData.shield);
            scale.OnlySetData(pointData.scale);
            rotation.OnlySetData(pointData.rotation);
            standingTime.OnlySetData(pointData.standingTime);
            travelTime.OnlySetData(pointData.travelTime);
            if (pointData.bulletIdArr == null) return;
            foreach (int id in pointData.bulletIdArr)
                bulletIdSet.Add(id);
        }
    }
}