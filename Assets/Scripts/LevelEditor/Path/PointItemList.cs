namespace SkyStrike
{
    namespace Editor
    {
        public class PointItemList : UIGroupDataPool<PointDataObserver>
        {
            public override UIElement<PointDataObserver> CreateItemAndAddData(PointDataObserver data)
            {
                var recentPoint = GetItem(items.Count - 1) as CurvePoint;
                var newPoint = base.CreateItemAndAddData(data) as CurvePoint;
                LinkNewPoint(recentPoint, newPoint,null);
                return newPoint;
            }
            public override void DisplayDataList()
            {
                Clear();
                var dataList = container?.GetDataList()?.GetList();
                if (dataList == null) return;
                foreach (var data in dataList)
                {
                    var recentPoint = GetItem(items.Count - 1) as CurvePoint;
                    var newPoint = CreateItem(data) as CurvePoint;
                    LinkNewPoint(recentPoint, newPoint,null);
                }
            }
            private void LinkNewPoint(CurvePoint prevPoint, CurvePoint newPoint, CurvePoint nextPoint)
            {
                newPoint.SetPrevPoint(prevPoint);
                newPoint.SetNextPoint(nextPoint);
            }
        }
    }
}