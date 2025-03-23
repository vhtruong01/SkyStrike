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
            private List<ISubMenu> subMenuList;
            private int curSubmenuIndex;

            public override void Awake()
            {
                base.Awake();
                EventManager.onSelectMetaObject.AddListener(SelectMetaObject);
            }
            public override void Init()
            {
                curSubmenuIndex = -1;
                subMenuList = new() { objectInfoMenu, phaseMenu, waveInfoMenu };
                for (int i = 0; i < subMenuList.Count; i++)
                    switchSubMenuBtnGroup.GetBaseItem(i).onSelectUI.AddListener(SelectSubMenu);
            }
            private void SelectSubMenu(int index)
            {
                if (curSubmenuIndex != index)
                {
                    if (curSubmenuIndex != -1)
                        subMenuList[curSubmenuIndex].Hide();
                    curSubmenuIndex = index;
                }
                subMenuList[index].Show();
            }
            protected override void CreateObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) => SelectObject(null);
            protected override void SelectObject(ObjectDataObserver data)
            {
                objectInfoMenu.Display(data);
                phaseMenu.Display(data?.phase);
                int menuIndex = curSubmenuIndex;
                if (menuIndex < 2)
                    menuIndex = data?.phase != null ? menuIndex : 0;
                else menuIndex = 0;
                switchSubMenuBtnGroup.SelectAndInvokeItem(menuIndex);
            }
            private void SelectMetaObject(ObjectDataObserver data)
            {
                objectInfoMenu.Display(data);
                phaseMenu.Display(null);
                switchSubMenuBtnGroup.SelectAndInvokeItem(0);
            }
            protected override void SelectWave(WaveDataObserver data)
            {
                SelectObject(null);
                waveInfoMenu.Display(data);
                switchSubMenuBtnGroup.SelectAndInvokeItem(2);
            }
        }
    }
}