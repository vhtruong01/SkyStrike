using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace SkyStrike.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private int maxDigit = 10;
        private TextMeshProUGUI text;
        private StringBuilder sb;
        private Coroutine coroutine;
        private int prevScore;

        private void Awake()
        {
            sb = new StringBuilder();
            text = GetComponent<TextMeshProUGUI>();
            prevScore = 0;
            text.text = GetScoreText(0);
        }
        public void UpdateScoreDisplay(int score)
        {
            if (score == 0) return;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(Display(score));
        }
        private IEnumerator Display(int curScore)
        {
            int deltaScore = curScore - prevScore;
            int tempScore = prevScore;
            float totalTime = 1.5f;
            float elapsedTime = 0;
            while (elapsedTime < totalTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                prevScore = (int)(tempScore + elapsedTime / totalTime * deltaScore);
                text.text = GetScoreText(prevScore);
                yield return null;
            }
            text.text = GetScoreText(curScore);
            coroutine = null;
            prevScore = curScore;
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