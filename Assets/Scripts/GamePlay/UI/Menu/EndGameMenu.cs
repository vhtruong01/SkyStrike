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
        [SerializeField] private Button homeBtn;
        private Image bg;
        private Animator animator;

        private void Awake()
        {
            animator = content.GetComponent<Animator>();
            bg = content.GetComponent<Image>();
            homeBtn.onClick.AddListener(() => StartCoroutine(GoHome()));
        }
        private IEnumerator GoHome()
        {
            EventManager.Active(EEventType.CloseScene);
            yield return new WaitForSeconds(0.5f);
            SceneSwapper.OpenMainMenu();
        }
        private void Display(EndGameEventData eventData)
        {
            animator.gameObject.SetActive(true);
            bg.color = eventData.isWin ? winColor : loseColor;
            title.text = eventData.isWin ? "Level complete!" : "Defeat";
            title.color = eventData.isWin ? winTitleColor : loseTitleColor;
            animator.SetTrigger(eventData.isWin ? "Win" : "Lose");
            StartCoroutine(DisplayScoreAndStar(animator.GetCurrentAnimatorStateInfo(0).length, eventData.score, eventData.star));
        }
        private IEnumerator DisplayScoreAndStar(float delay, float score, float star)
        {
            yield return new WaitForSecondsRealtime(delay);
            float duration = 1.5f;
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
                scoreText.text = "Score: " + Mathf.CeilToInt(score * elapsedTime / duration);
                starText.text = "Star: " + Mathf.CeilToInt(star * elapsedTime / duration);
            }
            scoreText.text = "Score: " + score;
            starText.text = "Star: " + star;

        }
        private void OnEnable()
            => EventManager.Subscribe<EndGameEventData>(Display);
        private void OnDisable()
            => EventManager.Unsubscribe<EndGameEventData>(Display);
    }
}