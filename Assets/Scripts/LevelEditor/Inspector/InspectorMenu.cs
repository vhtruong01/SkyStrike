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
            private ISubMenu curSubMenu;
            // frame data
            //
            private bool isLock;

            public void Start()
            {
                MenuManager.onSelectItemUI.AddListener(data =>
                {
                    objectInfoMenu.Display(data);
                    phaseMenu.Display(null);
                    SelectSubMenu(data != null ? objectInfoMenu : null);
                });
                MenuManager.onSelectEnemy.AddListener(data =>
                {
                    objectInfoMenu.Display(data);
                    phaseMenu.Display(data);
                    if (curSubMenu == null)
                        SelectSubMenu(objectInfoMenu);
                });
                subMenuList = new() { objectInfoMenu, phaseMenu };
                foreach (ISubMenu subMenu in subMenuList)
                {
                    subMenu.Show();
                    Button button = switchSubMenuBtnGroup.CreateItem<Button>();
                    ISubMenu curSubMenu = subMenu;
                    button.onClick.AddListener(() =>
                    {
                        switchSubMenuBtnGroup.SelectItem(button.gameObject);
                        SelectSubMenu(curSubMenu);
                    });
                    subMenu.Hide();
                }
            }
            public void SelectSubMenu(ISubMenu subMenu)
            {
                if (curSubMenu == subMenu)
                {
                    curSubMenu.Show();
                    return;
                }
                curSubMenu?.Hide();
                if (subMenu != null && subMenu.CanDisplay())
                {
                    curSubMenu = subMenu;
                    curSubMenu.Show();
                }
            }
        }
    }
}