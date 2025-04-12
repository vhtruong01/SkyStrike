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
            [SerializeField] private Button levelButton;
            [SerializeField] private Button shipBtn;
            [SerializeField] private Button settingBtn;
            [SerializeField] private Button exitBtn;
            [SerializeField] private Animator transition;
            [SerializeField] private Animator shipAnimator;

            public void Awake()
            {
                levelMenu.closeAction = () => StartCoroutine(EnterLevelMenu(false));
                levelButton.onClick.AddListener(() => StartCoroutine(EnterLevelMenu(true)));
                shipBtn.onClick.AddListener(shipMenu.Expand);
                settingBtn.onClick.AddListener(settingMenu.Expand);
                exitBtn.onClick.AddListener(exitMenu.Expand);
            }
            public IEnumerator EnterLevelMenu(bool isEnable)
            {
                transition.SetTrigger("Close");
                shipAnimator.SetTrigger("Disappear");
                yield return new WaitForSeconds(0.75f);
                levelMenu.gameObject.SetActive(isEnable);
                shipAnimator.SetTrigger("Appear");
                transition.SetTrigger("Open");
            }
        }
    }
}