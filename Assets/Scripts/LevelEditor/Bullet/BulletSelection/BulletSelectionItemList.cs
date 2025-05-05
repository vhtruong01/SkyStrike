namespace SkyStrike.Editor
{
    public class BulletSelectionItemList : UIGroupPool<BulletDataObserver>
    {
        private PointDataObserver pointDataObserver;

        public void Start()
            => selectDataCall = UpdateBullet;
        public void SetPoint(PointDataObserver point)
        {
            pointDataObserver = point;
            if (point == null) return;
            if (point.bulletId != BulletDataObserver.UNDEFINED_ID)
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i] as BulletSelectionItemUI;
                    if (point.bulletId == item.data.id)
                    {
                        SelectItem(i);
                        break;
                    }
                }
            else SelectNone();
        }
        public void UpdateBullet(BulletDataObserver bulletData)
        {
            if (pointDataObserver == null) return;
            pointDataObserver.SetBulletId(bulletData.id);
        }
    }
}