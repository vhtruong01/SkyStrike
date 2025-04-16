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
            public void FlipX()
            {
                float midX = points[0].midPos.data.x;
                for (var i = 0; i < points.Count; i++)
                {
                    points[i].prePos.SetData(new(2 * midX - points[i].prePos.data.x, points[i].prePos.data.y));
                    points[i].midPos.SetData(new(2 * midX - points[i].midPos.data.x, points[i].midPos.data.y));
                    points[i].nextPos.SetData(new(2 * midX - points[i].nextPos.data.x, points[i].nextPos.data.y));
                }
            }
            public MoveDataObserver Clone()
            {
                MoveDataObserver newData = new();
                newData.delay.OnlySetData(delay.data);
                newData.velocity.OnlySetData(velocity.data);
                if (points.Count == 0)
                    CreateEmpty(out _);
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
                {
                    var p = points[i].ExportData();
                    newData.points[i] = p;
                }
                return newData;
            }
            public void ImportData(MoveData moveData)
            {
                delay.OnlySetData(moveData.delay);
                velocity.OnlySetData(moveData.velocity);
                if (moveData == null || moveData.points.Length == 0) return;
                if (moveData.points != null)
                    for (int i = 0; i < moveData.points.Length; i++)
                        Add(new(moveData.points[i]));
            }
            public void GetList(out List<PointDataObserver> list) => list = points;
            public void CreateEmpty(out PointDataObserver newPoint)
            {
                newPoint = new();
                Add(newPoint);
            }
            public void Add(PointDataObserver data) => points.Add(data);
            public void Remove(PointDataObserver data)
            {
                data.UnbindAll();
                points.Remove(data);
            }
            public void Remove(int index, out PointDataObserver pointData)
            {
                pointData = points[index];
                pointData.UnbindAll();
                points.RemoveAt(index);
            }
            public void Set(int index, PointDataObserver data) => points[index] = data;
            public PointDataObserver First() => points[0];
        }
    }
}