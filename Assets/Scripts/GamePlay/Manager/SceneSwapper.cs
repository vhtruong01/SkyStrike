using DG.Tweening;
using UnityEngine.SceneManagement;

namespace SkyStrike
{
    public enum EScene
    {
        Loading = 0,
        MainMenu,
        MainGame,
        Editor
    }
    public static class SceneSwapper
    {
        private static void LoadScene(EScene sceneType)
        {
            DOTween.KillAll();
            SceneManager.LoadScene((int)sceneType);
        }
        public static void PlayGame() => LoadScene(EScene.MainGame);
        public static void OpenMainMenu() => LoadScene(EScene.MainMenu);
        public static void OpenEditor() => LoadScene(EScene.Editor);
    }
}