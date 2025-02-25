using UnityEngine;
using UnityEngine.UI;
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
            private List<ISubMenu> subMenuList;
            private int curSubMenuIndex;
            // frame data
            //
            private bool isLock;

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
                    Button button = switchSubMenuBtnGroup.CreateItem<Button>();
                    ISubMenu subMenuTmp = subMenu;
                    button.onClick.AddListener(() =>
                    {
                        switchSubMenuBtnGroup.SelectItem(button.gameObject);
                        subMenuTmp.Show();
                        SelectSubMenu(index);
                    });
                    subMenu.Hide();
                }
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
                subMenuList[index].Display(data);
                SelectSubMenu(index);
                for (int i = 0; i < subMenuList.Count; i++)
                    subMenuList[i].SetData(data);
            }
        }
    }
}