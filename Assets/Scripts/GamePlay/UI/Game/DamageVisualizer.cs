using SkyStrike.Game;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SkyStrike.UI
{
    public class DamageVisualizer : UIElement
    {
        private static readonly float displayTime = 0.2f;
        private static readonly float startSize = 1.5f;
        private TextMeshProUGUI text;

        private void Awake()
            => text = GetComponent<TextMeshProUGUI>();
        public override void Display(UIEventData eventData)
        {
            var data = eventData as DamageVisualizerEventData;
            var c = data.damageType switch
            {
                EDamageType.Normal => Color.white,
                EDamageType.Slashing => Color.cyan,
                EDamageType.Piercing => Color.magenta,
                _ => Color.red,
            };
            transform.position = data.position.SetZ(transform.position.z);
            text.text = data.damage.ToString();
            text.color = c;
            StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            float totalTime = displayTime;
            while (totalTime > 0)
            {
                totalTime -= Time.unscaledDeltaTime;
                transform.localScale = Vector3.one * (totalTime / displayTime * startSize + 1f);
                yield return null;
            }
            totalTime = displayTime;
            while (totalTime > 0)
            {
                totalTime -= Time.unscaledDeltaTime;
                text.color = text.color.ChangeAlpha(totalTime / displayTime);
                yield return null;
            }
            onDestroy.Invoke(this);
        }
    }
}