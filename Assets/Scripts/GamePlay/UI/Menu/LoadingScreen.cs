using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        [SerializeField] private Image icon;

        public void Start()
            => StartCoroutine(ShowLoadingProgess());
        public IEnumerator ShowLoadingProgess()
        {
            yield return new WaitForSeconds(0.5f);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)EScene.MainMenu);
            float totalProgress = 0;
            while (!asyncOperation.isDone)
            {
                totalProgress += asyncOperation.progress;
                progressBar.value = totalProgress;
                yield return null;
            }
            SceneManager.UnloadSceneAsync((int)EScene.Loading);
        }
    }
}