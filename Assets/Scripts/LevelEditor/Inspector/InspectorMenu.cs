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
                EventManager.onSelectMetaObject.AddListener(SelectObjectMeta);
                EventManager.onSelectObject.AddListener(SelectObject);
                EventManager.onSelectWave.AddListener(SelectWave);
            }
            public void Start()
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
                switchSubMenuBtnGroup.SelectFirstItem();
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
                switchSubMenuBtnGroup.SelectAndInvoke(index);
            }
            private void SelectObject(IEditorData data)
            {
                int index = curSubMenuIndex > 1 ? 0 : curSubMenuIndex;
                SelectAndSetDataSubMenu(data, index);
                subMenuList[1 - index].Display(data);
            }
            private void SelectObjectMeta(IEditorData data)
            {
                SelectAndSetDataSubMenu(data, 0);
                subMenuList[1].Display(null);
            }
            private void SelectWave(IEditorData data)
            {
                SelectObject(null);
                SelectAndSetDataSubMenu(data, 2);
            }
        }
    }
}