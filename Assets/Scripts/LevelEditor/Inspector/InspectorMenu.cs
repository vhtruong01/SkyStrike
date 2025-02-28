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
            [SerializeField] private UIGroup switchSubMenuBtnGroup;
            private bool isLock;
            private List<ISubMenu> subMenuList;
            private int curSubMenuIndex;
            // frame data

            public void Start()
            {
                curSubMenuIndex = -1;
                subMenuList = new() { objectInfoMenu, phaseMenu };
                MenuManager.onSelectItemUI.AddListener(data => SelectAndSetDataSubMenu(data, 0));
                MenuManager.onSelectEnemy.AddListener(SelectAndSetDataSubMenu);
                for (int i = 0; i < subMenuList.Count; i++)
                {
                    int index = i;
                    ISubMenu subMenu = subMenuList[i];
                    subMenu.gameObject.SetActive(true);
                    ISubMenu subMenuTmp = subMenu;
                    var button = switchSubMenuBtnGroup.GetItem(index);
                    button.onClick.AddListener(() =>
                    {
                        subMenuTmp.Show();
                        SelectSubMenu(index);
                    });
                    subMenu.Hide();
                }
                switchSubMenuBtnGroup.SelectFirstItem();
            }
            public void SelectSubMenu(int index)
            {
                if (curSubMenuIndex != index)
                {
                    ISubMenu subMenu = curSubMenuIndex >= 0 && curSubMenuIndex < subMenuList.Count ? subMenuList[curSubMenuIndex] : null;
                    subMenu?.Hide();
                    curSubMenuIndex = index;
                }
            }
            public void SelectAndSetDataSubMenu(IData data) => SelectAndSetDataSubMenu(data, curSubMenuIndex);
            public void SelectAndSetDataSubMenu(IData data, int index)
            {
                switchSubMenuBtnGroup.SelectItem(index);
                subMenuList[index].Display(data);
                SelectSubMenu(index);
                for (int i = 0; i < subMenuList.Count; i++)
                    subMenuList[i].SetData(data);
            }
        }
    }
}