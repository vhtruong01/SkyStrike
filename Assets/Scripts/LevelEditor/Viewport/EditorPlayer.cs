using UnityEngine;
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
            public void PlayGame() => EventManager.Play();
        }
    }
}