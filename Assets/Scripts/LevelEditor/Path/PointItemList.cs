namespace SkyStrike.Editor
{
    public class PointItemList : UIGroupDataPool<PointDataObserver>
    {
        public IScalableScreen screen { private get; set; }

        public void SetTypeToLastPoint(bool isStraight)
        {
            var point = GetItem(items.Count - 1) as CurvePoint;
            if (point != null)
                point.data.isStraightLine.OnlySetData(isStraight);
        }
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
            newPoint.SetPrevPoint(recentPoint);
            return newPoint;
        }
        public override void DisplayDataList()
        {
            Clear();
            container.GetDataList().GetList(out var dataList);
            if (dataList == null || dataList.Count == 0) return;
            foreach (var data in dataList)
            {
                var recentPoint = GetItem(items.Count - 1) as CurvePoint;
                var newPoint = CreateItem(data) as CurvePoint;
                newPoint.SetPrevPoint(recentPoint);
            }
        }
        public override bool RemoveSelectedItem()
        {
            if (selectedItemIndex == 0) return false;
            return base.RemoveSelectedItem();
        }
    }
}