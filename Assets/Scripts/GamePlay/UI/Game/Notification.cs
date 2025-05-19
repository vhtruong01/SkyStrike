using SkyStrike.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public enum ENoti
    {
        Danger,
        Warning,
        Safe,
    }
    public class Notification : UIElement
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI message;
        private Image bg;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            bg = GetComponent<Image>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public override void Display(UIEventData eventData)
        {
            var data = eventData as NotiEventData;
            icon.sprite = data.sprite;
            title.text = data.title;
            message.text = data.message;
            Color c = data.notiType switch
            {
                ENoti.Safe => Color.cyan,
                ENoti.Warning => Color.yellow,
                ENoti.Danger => Color.red,
                _ => Color.white
            };
            bg.color = c.ChangeAlpha(0.03f);
            StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            float time = 0.33f;
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                elapsedTime += Time.unscaledDeltaTime;
                canvasGroup.alpha = elapsedTime / time;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(time);
            elapsedTime = 0;
            while (elapsedTime < time)
            {
                elapsedTime += Time.unscaledDeltaTime;
                canvasGroup.alpha = 1 - elapsedTime / time;
                yield return null;
            }
            onDestroy.Invoke(this);
        }
    }
}