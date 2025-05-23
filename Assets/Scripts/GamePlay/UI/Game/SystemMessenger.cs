using SkyStrike.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SkyStrike.UI
{
    public class SystemMessenger : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Coroutine coroutine;
        private Queue<string> textQueue;

        private void Awake()
        {
            textQueue = new();
            text = GetComponent<TextMeshProUGUI>();
            text.text = "";
        }
        private void OnEnable()
            => EventManager.Subscribe<SystemMessengerEventData>(Display);
        private void OnDisable()
            => EventManager.Unsubscribe<SystemMessengerEventData>(Display);
        private void Display(SystemMessengerEventData eventData)
        {
            if (string.IsNullOrEmpty(eventData.text))
                return;
            textQueue.Enqueue(eventData.text);
            coroutine ??= StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            while (textQueue.Count > 0)
            {
                string txt = textQueue.Dequeue();
                SoundManager.PlaySound(ESound.Text);
                var w = new WaitForSecondsRealtime(1f / txt.Length);
                for (int i = 0; i < txt.Length; i++)
                {
                    text.text += txt[i];
                    yield return w;
                }
                yield return new WaitForSecondsRealtime(1.5f);
                SoundManager.PlaySound(ESound.Text);
                if (txt.Length > 15)
                {
                    w = new WaitForSecondsRealtime(2f / txt.Length);
                    for (int i = txt.Length / 2; i > 0; i--)
                    {
                        text.text = text.text[1..^1];
                        yield return w;
                    }
                }
                else
                {
                    w = new WaitForSecondsRealtime(1f / txt.Length);
                    for (int i = 1; i < txt.Length; i++)
                    {
                        text.text = text.text[..^1];
                        yield return w;
                    }
                }
                text.text = "";
                yield return new WaitForSecondsRealtime(0.5f);
            }
            coroutine = null;
        }
    }
}