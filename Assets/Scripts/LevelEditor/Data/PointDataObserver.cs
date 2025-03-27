using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Editor
    {
        public class PointDataObserver : IEditorData<Point, PointDataObserver>
        {
            public DataObserver<Vector2> prePos { get; private set; }
            public DataObserver<Vector2> midPos { get; private set; }
            public DataObserver<Vector2> nextPos { get; private set; }
            public DataObserver<bool> isTraight { get; private set; }

            public PointDataObserver()
            {
                prePos = new();
                midPos = new();
                nextPos = new();
                isTraight = new();
                prePos.SetData(new(2, 0));
                nextPos.SetData(new(0, 2));
            }
            public PointDataObserver(Point data) : this() => ImportData(data);
            public PointDataObserver Clone()
            {
                PointDataObserver newPoint = new();
                newPoint.prePos.SetData(prePos.data);
                newPoint.midPos.SetData(midPos.data);
                newPoint.nextPos.SetData(nextPos.data);
                newPoint.isTraight.SetData(isTraight.data);
                return newPoint;
            }
            public Point ExportData()
            {
                return new()
                {
                    isStraight = isTraight.data,
                    prevPos = new(prePos.data),
                    midPos = new(midPos.data),
                    nextPos = new(nextPos.data)
                };
            }
            public void ImportData(Point data)
            {
                prePos.SetData(data.prevPos.ToVector2());
                midPos.SetData(data.midPos.ToVector2());
                nextPos.SetData(data.nextPos.ToVector2());
                isTraight.SetData(data.isStraight);
            }
        }
    }
}