using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Menu levelMenu;
        [SerializeField] private Menu homeMenu;
        [SerializeField] private Menu settingMenu;
        [SerializeField] private Menu helpMenu;
        [SerializeField] private Menu exitMenu;
        [SerializeField] private Button levelButton;
        [SerializeField] private Button helpBtn;
        [SerializeField] private Button settingBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private Animator transition;
        [SerializeField] private Animator shipAnimator;

        public void Awake()
        {
            levelButton.onClick.AddListener(() => StartCoroutine(PlayGame()));
            helpBtn.onClick.AddListener(helpMenu.Expand);
            settingBtn.onClick.AddListener(settingMenu.Expand);
            exitBtn.onClick.AddListener(exitMenu.Expand);
        }
        public IEnumerator PlayGame()
        {
            homeMenu.Collapse();
            shipAnimator.SetTrigger("Disappear");
            float shipTravelTime = shipAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(shipTravelTime - 0.5f);
            transition.SetTrigger("Close");
            float transitionTime = transition.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(transitionTime);
            transition.SetTrigger("Open");
            SceneSwapper.PlayGame();
        }
    }
}