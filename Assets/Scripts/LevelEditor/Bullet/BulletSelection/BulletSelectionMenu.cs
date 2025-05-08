using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace SkyStrike.Editor
{
    public class BulletSelectionMenu : Menu, IPointerClickHandler
    {
        private List<BulletDataObserver> bulletList;
        private BulletSelectionItemList group;

        public void Refresh()
        {
            if (bulletList == null) return;
            group.Clear();
            for (int i = 0; i < bulletList.Count; i++)
                group.CreateItem(bulletList[i]);
        }
        protected override void Preprocess()
        {
            EventManager.onSelectLevel.AddListener(SelectLevel);
            group = GetComponent<BulletSelectionItemList>();
        }
        private void SelectLevel(LevelDataObserver levelDataObserver)
            => levelDataObserver.GetList(out bulletList);
        public void SelectPoint(PointDataObserver pointDataObserver)
            => group.SetPoint(pointDataObserver);
        public void OnPointerClick(PointerEventData eventData) { }
    }
}