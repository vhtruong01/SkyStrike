using DG.Tweening.Core.Easing;
using SkyStrike.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private HomeMenu homeMenu;
        [SerializeField] private LevelMenu levelMenu;
        [SerializeField] private ShipMenu shipMenu;
        [SerializeField] private SettingMenu settingMenu;
        [SerializeField] private ExitMenu exitMenu;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Button levelButton;
        [SerializeField] private Button shipBtn;
        [SerializeField] private Button settingBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private Animator transition;
        [SerializeField] private Animator shipAnimator;

        public void Awake()
        {
            levelButton.onClick.AddListener(() => StartCoroutine(EnterLevelMenu()));
            shipBtn.onClick.AddListener(shipMenu.Expand);
            settingBtn.onClick.AddListener(settingMenu.Expand);
            exitBtn.onClick.AddListener(exitMenu.Expand);
        }
        public IEnumerator EnterLevelMenu()
        {
            transition.SetTrigger("Close");
            shipAnimator.SetTrigger("Disappear");
            yield return new WaitForSeconds(0.75f);
            levelMenu.gameObject.SetActive(true);
            transition.SetTrigger("Open");
        }
        public IEnumerator CloseLevelMenu()
        {
            transition.SetTrigger("Close");
            yield return new WaitForSeconds(0.5f);
            levelMenu.gameObject.SetActive(false);
            shipAnimator.SetTrigger("Appear");
            transition.SetTrigger("Open");
        }
        public IEnumerator PlayGame(LevelData levelData)
        {
            gameManager.curLevel = levelData;
            transition.SetTrigger("Close");
            yield return new WaitForSeconds(0.5f);
            SceneSwapper.PlayGame();
        }
    }
}