namespace SkyStrike
{
    namespace Editor
    {
        public class PointItemList : UIGroupDataPool<PointDataObserver>
        {
            public GridScreen screen { private get; set; }

            protected override UIElement<PointDataObserver> CreateObject()
            {
                var item = base.CreateObject() as CurvePoint;
                item.screen = screen;
                return item;
            }
            public override UIElement<PointDataObserver> CreateItemAndAddData(PointDataObserver data)
            {
                var recentPoint = GetItem(items.Count - 1) as CurvePoint;
                var newPoint = base.CreateItemAndAddData(data) as CurvePoint;
                LinkNewPoint(recentPoint, newPoint, null);
                return newPoint;
            }
            public override void DisplayDataList()
            {
                Clear();
                var dataList = container?.GetDataList()?.GetList();
                if (dataList == null || dataList.Count == 0) return;
                foreach (var data in dataList)
                {
                    var recentPoint = GetItem(items.Count - 1) as CurvePoint;
                    var newPoint = CreateItem(data) as CurvePoint;
                    LinkNewPoint(recentPoint, newPoint, null);
                }
            }
            private void LinkNewPoint(CurvePoint prevPoint, CurvePoint newPoint, CurvePoint nextPoint)
            {
                newPoint.SetPrevPoint(prevPoint);
                newPoint.SetNextPoint(nextPoint);
            }
            public override bool RemoveSelectedItem()
            {
                if (selectedItemIndex == 0) return false;
                return base.RemoveSelectedItem();
            }
        }
    }
}