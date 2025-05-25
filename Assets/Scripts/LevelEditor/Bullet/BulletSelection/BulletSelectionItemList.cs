namespace SkyStrike.Editor
{
    public class BulletSelectionItemList : UIGroupPool<BulletDataObserver>
    {
        private PointDataObserver pointData;

        public void Start()
            => selectDataCall = UpdateBullet;
        public void SetPoint(PointDataObserver point)
        {
            pointData = point;
            if (point == null) return;
            var selectedItem = GetSelectedItem() as BulletSelectionItemUI;
            if (selectedItem != null && point.bulletId == selectedItem.data.id)
                return;
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
            if (pointData == null) return;
            pointData.SetBulletId(bulletData?.id);
        }
    }
}