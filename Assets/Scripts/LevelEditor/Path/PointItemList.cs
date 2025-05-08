namespace SkyStrike.Editor
{
    public class PointItemList : UIGroupPool<PointDataObserver>
    {
        public IScalableScreen screen { private get; set; }

        public void FlipX()
        {
            (dataList as MoveDataObserver)?.FlipX();
            RefreshDataList();
        }
        public void SetTypeToLastPoint(bool isStraight)
        {
            var point = GetItem(items.Count - 1) as Point;
            if (point != null)
                point.data.isStraightLine.OnlySetData(isStraight);
        }
        public override UIElement<PointDataObserver> CreateItem(PointDataObserver data)
        {
            var recentPoint = GetItem(items.Count - 1) as Point;
            var newPoint = base.CreateItem(data) as Point;
            newPoint.SetPrevPoint(recentPoint);
            return newPoint;
        }

        public void RemoveDataList()
        {
            dataList.GetList(out var list);
            var pos = list[0];
            list.Clear();
            CreateItemAndAddData(pos);
            RefreshDataList();
        }
        public bool RemovePoint()
        {
            if (selectedItemIndex == 0) return false;
            return RemoveSelectedItem();
        }
    }
}