using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveDataObserver : IDataList<PointDataObserver>, IEditorData<MoveData, MoveDataObserver>
        {
            private List<PointDataObserver> points;
            public DataObserver<float> delay { get; private set; }
            public DataObserver<float> velocity { get; private set; }

            public MoveDataObserver()
            {
                points = new();
                delay = new();
                velocity = new();
            }
            public MoveDataObserver(MoveData moveData) : this() => ImportData(moveData);
            public void Translate(Vector2 pos)
            {
                var dir = pos - points[0].midPos.data;
                foreach (var point in points)
                    point.Translate(dir);
            }
            public MoveDataObserver Clone()
            {
                MoveDataObserver newData = new();
                newData.delay.OnlySetData(delay.data);
                newData.velocity.OnlySetData(velocity.data);
                if (points.Count == 0)
                    CreateEmpty();
                for (int i = 0; i < points.Count; i++)
                    newData.points.Add(points[i].Clone());
                return newData;
            }
            public MoveData ExportData()
            {
                MoveData newData = new()
                {
                    delay = delay.data,
                    velocity = velocity.data,
                    points = new Point[points.Count]
                };
                for (int i = 0; i < points.Count; i++)
                    newData.points[i] = points[i].ExportData();
                return newData;
            }
            public void ImportData(MoveData moveData)
            {
                delay.OnlySetData(moveData.delay);
                velocity.OnlySetData(moveData.velocity);
                if (moveData == null || moveData.points.Length == 0) return;
                for (int i = 0; i < moveData.points.Length; i++)
                    Add(new(moveData.points[i]));
            }
            public List<PointDataObserver> GetList() => points;
            public PointDataObserver CreateEmpty()
            {
                PointDataObserver newPoint = new();
                Add(newPoint);
                return newPoint;
            }
            public void Add(PointDataObserver data) => points.Add(data);
            public void Remove(PointDataObserver data)
            {
                data.UnbindAll();
                points.Remove(data);
            }
            public void Remove(int index)
            {
                points[index].UnbindAll();
                points.RemoveAt(index);
            }
            public void Swap(int leftIndex, int rightIndex) => points.Swap(leftIndex, rightIndex);
            public void Set(int index, PointDataObserver data) => points[index] = data;
            public PointDataObserver First() => points[0];
        }
    }
}