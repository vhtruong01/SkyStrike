using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace SkyStrike.Editor
{
    public class BulletSelectionMenu : Menu, IPointerClickHandler
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
        protected override void Preprocess()
        {
            EventManager.onSelectLevel.AddListener(SelectLevel);
            bulletSelectionItemList = GetComponent<BulletSelectionItemList>();
        }
        private void SelectLevel(LevelDataObserver levelDataObserver)
            => levelDataObserver.GetList(out bulletList);
        public void SelectPoint(PointDataObserver pointDataObserver)
            => bulletSelectionItemList.SetPoint(pointDataObserver);
        public void OnPointerClick(PointerEventData eventData) { }
    }
}