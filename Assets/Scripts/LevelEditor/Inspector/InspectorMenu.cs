using UnityEngine;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class InspectorMenu : Menu
        {
            [SerializeField] private ObjectInfoMenu objectInfoMenu;
            [SerializeField] private PhaseMenu phaseMenu;
            [SerializeField] private WaveInfoMenu waveInfoMenu;
            [SerializeField] private UIGroup switchSubMenuBtnGroup;
            private bool isLock;
            private List<ISubMenu> subMenuList;
            private int curSubMenuIndex;

            public override void Awake()
            {
                base.Awake();
                subMenuList = new() { objectInfoMenu, phaseMenu, waveInfoMenu };
                curSubMenuIndex = 0;
                EventManager.onSelectMetaObject.AddListener(SelectMetaObject);
            }
            public override void Init()
            {
                for (int i = 0; i < subMenuList.Count; i++)
                {
                    ISubMenu subMenu = subMenuList[i];
                    subMenu.gameObject.SetActive(true);
                    ISubMenu subMenuTmp = subMenu;
                    var button = switchSubMenuBtnGroup.GetItem(i);
                    button.onSelectUI.AddListener(SelectSubMenu);
                    subMenu.Hide();
                }
            }
            private void SelectSubMenu(int index)
            {
                if (curSubMenuIndex != index)
                {
                    subMenuList[curSubMenuIndex].Hide();
                    curSubMenuIndex = index;
                }
                subMenuList[curSubMenuIndex].Show();
            }
            private void SelectAndSetDataSubMenu(IEditorData data, int index)
            {
                if (index < 0 || index >= subMenuList.Count) return;
                subMenuList[index].Display(data);
                switchSubMenuBtnGroup.SelectAndInvokeItem(index);
            }
            protected override void SelectObject(IEditorData data)
            {
                int index = curSubMenuIndex > 1 ? 0 : curSubMenuIndex;
                SelectAndSetDataSubMenu(data, index);
                subMenuList[1 - index].Display(data);
            }
            private void SelectMetaObject(IEditorData data)
            {
                SelectAndSetDataSubMenu(data, 0);
                subMenuList[1].Display(null);
            }
            protected override void SelectWave(IEditorData data)
            {
                SelectObject(null);
                SelectAndSetDataSubMenu(data, 2);
            }
            protected override void CreateObject(IEditorData data) { }
            protected override void RemoveObject(IEditorData data)
            {
            }
        }
    }
}