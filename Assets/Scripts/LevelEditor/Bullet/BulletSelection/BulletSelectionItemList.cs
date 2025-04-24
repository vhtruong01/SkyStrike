namespace SkyStrike.Editor
{
    public class BulletSelectionItemList : UIGroupPool<BulletDataObserver>
    {
        private PointDataObserver pointDataObserver;

        public override void Init()
        {
            base.Init();
            selectDataCall = UpdateBullet;
        }
        public void SetPoint(PointDataObserver point)
        {
            pointDataObserver = point;
            if (point == null) return;
            SelectNone();
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i] as BulletSelectionItemUI;
                item.Check(point.bulletId == item.data.id);
            }
        }
        public void UpdateBullet(BulletDataObserver bulletData)
        {
            if (pointDataObserver == null) return;
            //(GetSelectedItem() as BulletSelectionItemUI)?.Check(false);
            pointDataObserver.SetBulletId(bulletData.id);
        }
    }
}