using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                    subMenu.gameObject.SetActive(false);
                    Button button = switchSubMenuBtnGroup.CreateItem<Button>();
                    button.onClick.AddListener(() =>
                    {
                        switchSubMenuBtnGroup.SelectItem(button.gameObject);
                        SelectSubMenu(subMenu);
                    });
                }
            }
            public void SelectSubMenu(ISubMenu subMenu)
            {
                if (curSubMenu == subMenu) return;
                curSubMenu?.gameObject.SetActive(false);
                curSubMenu = subMenu;
                if (curSubMenu != null && curSubMenu.CanDisplay())
                    curSubMenu.gameObject.SetActive(true);
            }

        }
    }
}