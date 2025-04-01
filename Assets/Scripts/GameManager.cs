using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using SkyStrike.UI;
using System.Collections;

namespace SkyStrike
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        [SerializeField] private LoadingScreen loadingScene;
        [SerializeField] private Camera cam;
        private EScene curScene;
        private List<AsyncOperation> scenesLoading;

        //
        private void Awake()
        {
            instance = this;
            curScene = EScene.None;
            scenesLoading = new();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        public void Start() => LoadScene(EScene.MainMenu);
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => SceneManager.SetActiveScene(scene);
        public IEnumerator DisplayLoadingScene()
        {
            cam.gameObject.SetActive(true);
            loadingScene.gameObject.SetActive(true);
            yield return StartCoroutine(loadingScene.ShowLoadingProgess(scenesLoading));
            cam.gameObject.SetActive(false);
            loadingScene.gameObject.SetActive(false);
        }
        public static void LoadScene(EScene scene)
        {
            if (instance == null)
                SceneManager.LoadScene((int)scene);
            else
            {
                instance.scenesLoading.Clear();
                if (instance.curScene != EScene.None)
                    instance.scenesLoading.Add(SceneManager.UnloadSceneAsync((int)instance.curScene));
                instance.scenesLoading.Add(SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive));
                instance.curScene = scene;
                instance.StartCoroutine(instance.DisplayLoadingScene());
            }

        }
    }
}