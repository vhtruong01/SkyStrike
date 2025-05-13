using SkyStrike.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class EndGameMenu : MonoBehaviour
    {
        private static readonly Color winColor = new(0, 0.2f, 0.2f, 0.75f);
        private static readonly Color winTitleColor = new(0, 1, 1);
        private static readonly Color loseColor = new(0.2f, 0, 0, 0.75f);
        private static readonly Color loseTitleColor = new(1, 0, 0);
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI starText;
        [SerializeField] private GameObject content;
        private Image bg;
        private Button btn;
        private Animator animator;

        private void Awake()
        {
            animator = content.GetComponent<Animator>();
            bg = content.GetComponent<Image>();
            btn = content.GetComponent<Button>();
            btn.onClick.AddListener(() => SceneSwapper.OpenMainMenu());
        }
        private void Display(EndGameEventData eventData)
        {
            animator.gameObject.SetActive(true);
            bg.color = eventData.isWin ? winColor : loseColor;
            title.text = eventData.isWin ? "Level complete!" : "Defeat";
            title.color = eventData.isWin ? winTitleColor : loseTitleColor;
            animator.SetTrigger(eventData.isWin ? "Win" : "Lose");
        }
        private void OnEnable()
            => EventManager.Subscribe<EndGameEventData>(Display);
        private void OnDisable()
            => EventManager.Unsubscribe<EndGameEventData>(Display);
    }
}