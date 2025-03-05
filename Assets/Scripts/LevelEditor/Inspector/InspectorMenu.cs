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

            public void Awake()
            {
                subMenuList = new() { objectInfoMenu, phaseMenu, waveInfoMenu };
            }
            public override void Start()
            {
                base.Start();
                curSubMenuIndex = 0;
                MenuManager.onSelectItemUI.AddListener(SelectEnemyMeta);
                MenuManager.onSelectEnemy.AddListener(SelectEnemy);
                MenuManager.onSelectWave.AddListener(SelectWave);
                for (int i = 0; i < subMenuList.Count; i++)
                {
                    int index = i;
                    ISubMenu subMenu = subMenuList[i];
                    subMenu.gameObject.SetActive(true);
                    ISubMenu subMenuTmp = subMenu;
                    var button = switchSubMenuBtnGroup.GetItem(index);
                    button.onClick.AddListener(() => SelectSubMenu(index));
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
            private void SelectAndSetDataSubMenu(IData data, int index)
            {
                if (index < 0 || index >= subMenuList.Count) return;
                subMenuList[index].Display(data);
                switchSubMenuBtnGroup.GetItem(index).onClick.Invoke();
                switchSubMenuBtnGroup.GetItem(index).onSelectUI.Invoke();
            }
            public void SelectEnemy(IData data)
            {
                int index = curSubMenuIndex > 1 ? 0 : curSubMenuIndex;
                SelectAndSetDataSubMenu(data, index);
                subMenuList[1 - index].SetData(data);
            }
            public void SelectEnemyMeta(IData data)
            {
                SelectAndSetDataSubMenu(data, 0);
                subMenuList[1].SetData(null);
            }
            public void SelectWave(IData data)
            {
                SelectAndSetDataSubMenu(data, 2);
            }
        }
    }
}