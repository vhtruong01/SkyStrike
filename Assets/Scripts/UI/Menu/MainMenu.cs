using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public class MainMenu : MonoBehaviour
        {
            [SerializeField] private HomeMenu homeMenu;
            [SerializeField] private LevelMenu levelMenu;
            [SerializeField] private ShipMenu shipMenu;
            [SerializeField] private SettingMenu settingMenu;
            [SerializeField] private ExitMenu exitMenu;
            [SerializeField] private Button lvButton;
            [SerializeField] private Button shipBtn;
            [SerializeField] private Button settingBtn;
            [SerializeField] private Button exitBtn;
            [SerializeField] public Animator transition;

            private float transitionTime = 1f;

            public void Start()
            {
                lvButton.onClick.AddListener(() => StartCoroutine(EnterLevelMenu()));
                settingBtn.onClick.AddListener(() => OpenMenu(settingMenu));
                exitBtn.onClick.AddListener(() => OpenMenu(exitMenu));
                shipBtn.onClick.AddListener(() => OpenMenu(shipMenu));
                shipMenu.closeAction = settingMenu.closeAction = exitMenu.closeAction = OpenHomeMenu;
            }
            private void OpenMenu(Menu menu)
            {
                menu.gameObject.SetActive(true);
                if (menu != homeMenu)
                    StartCoroutine(homeMenu.Close());
            }
            private void OpenHomeMenu() => homeMenu.gameObject.SetActive(true);
            public IEnumerator EnterLevelMenu()
            {
                transition.SetTrigger("Fade");
                yield return new WaitForSeconds(transitionTime);


            }
        }
    }
}