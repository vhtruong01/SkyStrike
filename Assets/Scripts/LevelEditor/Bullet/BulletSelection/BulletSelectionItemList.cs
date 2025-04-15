namespace SkyStrike
{
    namespace Editor
    {
        public class BulletSelectionItemList : UIGroupPool<BulletDataObserver>
        {
            private PointDataObserver pointDataObserver;

            public override void Awake()
            {
                base.Awake();
                selectDataCall = UpdateBulletList;
            }
            public void SetPoint(PointDataObserver point)
            {
                pointDataObserver = point;
                if (point == null) return;
                SelectNone();
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i] as BulletSelectionItemUI;
                    item.Check(point.bulletIdSet.Contains(item.data.id));
                }
            }
            public void UpdateBulletList(BulletDataObserver bulletData)
            {
                if (pointDataObserver == null) return;
                if (pointDataObserver.bulletIdSet.Contains(bulletData.id))
                    pointDataObserver.bulletIdSet.Remove(bulletData.id);
                else
                    pointDataObserver.bulletIdSet.Add(bulletData.id);
            }
        }
    }
}