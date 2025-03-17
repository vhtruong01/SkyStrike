using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class EditorPlayer : MonoBehaviour
        {
            [SerializeField] private Button playGameButton;
            public void Awake()
            {
                playGameButton.onClick.AddListener(PlayGame);
            }
            public void PlayGame()
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}