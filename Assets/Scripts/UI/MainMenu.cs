using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public class MainMenu : MonoBehaviour
        {
            [SerializeField] private Button lvButton;
            [SerializeField] private Button shipBtn;
            [SerializeField] private Button settingBtn;
            [SerializeField] private Button exitBtn;
            [SerializeField] public Animator transition;
            private float transitionTime = 1f;

            public void Awake()
            {
                lvButton.onClick.AddListener(() => StartCoroutine(EnterLevelMenu()));
            }
            public IEnumerator EnterLevelMenu()
            {
                transition.SetTrigger("Fade");
                yield return new WaitForSeconds(transitionTime);

            }
        }
    }
}