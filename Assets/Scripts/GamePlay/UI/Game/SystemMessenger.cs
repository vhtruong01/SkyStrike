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
        private WaitForSecondsRealtime waitForSecondsRealtime;
        private WaitForSecondsRealtime waitForSecondsRealtime2;
        private Queue<string> textQueue;

        private void Awake()
        {
            textQueue = new();
            text = GetComponent<TextMeshProUGUI>();
            text.text = "";
            waitForSecondsRealtime = new(0.05f);
            waitForSecondsRealtime2 = new(0.1f);
        }
        private void OnEnable()
            => EventManager.Subscribe<SystemMessengerEventData>(Display);
        private void OnDisable()
            => EventManager.Unsubscribe<SystemMessengerEventData>(Display);
        private void Display(SystemMessengerEventData eventData)
        {
            textQueue.Enqueue(eventData.text);
            coroutine ??= StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            while (textQueue.Count > 0)
            {
                string txt = textQueue.Dequeue();
                for (int i = 0; i < txt.Length; i++)
                {
                    text.text += txt[i];
                    yield return waitForSecondsRealtime;
                }
                yield return new WaitForSecondsRealtime(1.5f);
                while (txt.Length > 2)
                {
                    txt = txt[1..^1];
                    text.text = txt;
                    yield return waitForSecondsRealtime2;
                }
                text.text = "";
                yield return new WaitForSecondsRealtime(0.5f);
            }
            coroutine = null;
        }
    }
}