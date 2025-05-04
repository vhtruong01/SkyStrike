using SkyStrike.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject shipMenu;
        [SerializeField] private GameObject settingMenu;
        [SerializeField] private GameObject exitMenu;
        [SerializeField] private GameObject levelMenu;
        [SerializeField] private Button levelButton;
        [SerializeField] private Button shipBtn;
        [SerializeField] private Button settingBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private Animator transition;
        [SerializeField] private Animator shipAnimator;

        public void Awake()
        {
            levelButton.onClick.AddListener(() => StartCoroutine(PlayGame()));
            shipBtn.onClick.AddListener(() => shipMenu.SetActive(true));
            settingBtn.onClick.AddListener(() => settingMenu.SetActive(true));
            exitBtn.onClick.AddListener(() => exitMenu.SetActive(true));
        }
        public IEnumerator PlayGame()
        {
            levelMenu.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            transition.SetTrigger("Close");
            shipAnimator.SetTrigger("Disappear");
            yield return new WaitForSeconds(0.75f);
            transition.SetTrigger("Open");
            SceneSwapper.PlayGame();
        }
    }
}