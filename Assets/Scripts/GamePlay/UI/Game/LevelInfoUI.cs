using SkyStrike.Game;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LevelInfoUI : MonoBehaviour
    {
        [SerializeField] private int maxDigit = 10;
        [SerializeField] private Slider levelProgress1;
        [SerializeField] private Slider levelProgress2;
        [SerializeField] private TextMeshProUGUI scoreText;
        private StringBuilder sb;
        private Coroutine coroutine;
        private int score;

        private void Awake()
        {
            sb = new StringBuilder();
            score = 0;
            scoreText.text = GetScoreText(0);
            levelProgress1.value = levelProgress2.value = 0;
        }
        private void OnEnable()
            => EventManager.Subscribe<LevelProgressEventData>(Display);
        private void OnDisable()
            => EventManager.Unsubscribe<LevelProgressEventData>(Display);
        private void Display(LevelProgressEventData eventData)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(DisplayScore(score, eventData.score));
            score += eventData.score;
            if (eventData.percentRequired >= eventData.percent)
                levelProgress1.value = eventData.percent / eventData.percentRequired;
            else levelProgress2.value =Mathf.Min(1, (eventData.percent - eventData.percentRequired) / (1 - eventData.percentRequired));
        }
        private IEnumerator DisplayScore(int curScore, int deltaScore)
        {
            float totalTime = 1.5f;
            float elapsedTime = 0;
            while (elapsedTime < totalTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                scoreText.text = GetScoreText((int)(curScore + elapsedTime / totalTime * deltaScore));
                yield return null;
            }
            scoreText.text = GetScoreText(curScore + deltaScore);
            coroutine = null;
        }
        private string GetScoreText(int score)
        {
            sb.Clear();
            string s = score.ToString();
            sb.Append('0', maxDigit - s.Length).Append(s);
            return sb.ToString();
        }
    }
}