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
        private Vector3 scale;
        private TextMeshProUGUI text;

        private void Awake()
            => text = GetComponent<TextMeshProUGUI>();
        public override void Display(UIEventData eventData)
        {
            var data = eventData as DamageVisualizerEventData;
            var c = data.damageType switch
            {
                EDamageType.Normal => Color.white,
                EDamageType.Piercing => Color.yellow,
                EDamageType.Slashing => Color.cyan,
                EDamageType.MegaDamage => new(1, 0.5f, 0),
                _ => Color.red,
            };
            scale = (data.damageType == EDamageType.MegaDamage ? 2f : 1) * Vector3.one;
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
                totalTime -= Time.deltaTime;
                transform.localScale = scale * (totalTime / displayTime * startSize + 1f);
                yield return null;
            }
            totalTime = displayTime;
            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                text.color = text.color.ChangeAlpha(totalTime / displayTime);
                yield return null;
            }
            onDestroy.Invoke(this);
        }
    }
}