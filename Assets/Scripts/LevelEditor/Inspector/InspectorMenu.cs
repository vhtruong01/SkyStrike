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
                MenuManager.onSelectItemUI.AddListener(SelectDefaultSubMenu);
                MenuManager.onSelectEnemy.AddListener(SelectDefaultSubMenu);
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
            public void SelectDefaultSubMenu(IData data)
            {
                subMenuList[0].Display(data);
                SelectSubMenu(0);
                for (int i = 1; i < subMenuList.Count; i++)
                    subMenuList[i].SetData(data);
            }
        }
    }
}