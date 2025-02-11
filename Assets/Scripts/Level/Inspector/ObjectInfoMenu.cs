using SkyStrike.Enemy;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : Menu
        {
            [SerializeField] private InspectorMenu inspectorMenu;
            [SerializeField] private PhaseMenu phaseMenu;
            [SerializeField] private UIGroup switchSubMenuBtnGroup;
            private GameObject curSubMenu;
            private IEnemyData enemyData;
            // frame data
            //
            private bool isLock;

            public override void Awake()
            {
                base.Awake();
                MenuManager.onSelectEnemy.AddListener(data =>
                {
                    if (enemyData == data) return;
                    enemyData = data;
                    inspectorMenu.DisplayInfo(data);

                });
                MenuManager.onSelectItemUI.AddListener(data =>
                {
                    enemyData = data;
                    SelectSubMenu(data == null ? null : inspectorMenu.gameObject);
                    inspectorMenu.DisplayInfo(data);
                });

            }
            public void OnEnable()
            {
                inspectorMenu.gameObject.SetActive(false);
                phaseMenu.gameObject.SetActive(false);
            }
            public void SelectSubMenu(GameObject subMenu)
            {
                curSubMenu?.SetActive(false);
                curSubMenu = subMenu;
                if (curSubMenu != null)
                    curSubMenu.SetActive(true);
            }

        }
    }
}