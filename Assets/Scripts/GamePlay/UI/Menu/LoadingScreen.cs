using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        [SerializeField] private GameObject warningObj;
        private bool isLoaded = false;

        public void Update()
        {
            if (isLoaded) return;
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                warningObj.SetActive(false);
                StartCoroutine(ShowLoadingProgess());
                isLoaded = true;
            }
        }
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