using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletSelectionMenu : Menu
        {
            private List<BulletDataObserver> bulletList;
            private BulletSelectionItemList bulletSelectionItemList;

            public void Refresh()
            {
                if (bulletList == null) return;
                bulletSelectionItemList.Clear();
                for (int i = 0; i < bulletList.Count; i++)
                    bulletSelectionItemList.CreateItem(bulletList[i]);
            }
            public override void Init()
            {
                bulletSelectionItemList = GetComponent<BulletSelectionItemList>();
                EventManager.onSelectLevel.AddListener(SelectLevel);
            }
            private void SelectLevel(LevelDataObserver levelDataObserver)
                => levelDataObserver.GetList(out bulletList);
            public void SelectPoint(PointDataObserver pointDataObserver)
                => bulletSelectionItemList.SetPoint(pointDataObserver);
        }
    }
}