using SkyStrike.Game;
using System.Collections.Generic;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveDataObserver : IDataList<PointDataObserver>, IEditorData<MoveData, MoveDataObserver>
        {
            private List<PointDataObserver> points;

            public MoveDataObserver() : base() => points = new();
            public MoveDataObserver(MoveData moveData) : this() => ImportData(moveData);
            public MoveDataObserver Clone()
            {
                MoveDataObserver newData = new();
                CreateEmpty();
                for (int i = 0; i < points.Count; i++)
                    newData.points.Add(points[i].Clone());
                return newData;
            }
            public MoveData ExportData()
            {
                MoveData data = new();
                data.points = new Point[points.Count];
                for (int i = 0; i < points.Count; i++)
                    data.points[i] = points[i].ExportData();
                return data;
            }
            public void ImportData(MoveData moveData)
            {
                if (moveData == null || moveData.points.Length == 0) return;
                for (int i = 0; i < moveData.points.Length; i++)
                {
                    Add(new(moveData.points[i]));
                }
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