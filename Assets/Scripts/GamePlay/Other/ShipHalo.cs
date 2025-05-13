using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipHalo : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        public void Awake()
            => spriteRenderer = GetComponent<SpriteRenderer>();
        private void OnEnable()
            => EventManager.Subscribe(EEventType.PrepareGame, Hide);
        private void OnDisable()
            => EventManager.Unsubscribe(EEventType.PrepareGame, Hide);
        private void Hide()
            => StartCoroutine(Disappear());
        private IEnumerator Disappear()
        {
            float elapsedTime = 0;
            float time = 1f;
            var scale = transform.localScale;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                float delta = elapsedTime / time;
                transform.localScale = (1 + 0.75f * delta) * scale;
                spriteRenderer.color = spriteRenderer.color.ChangeAlpha(1 - delta);
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}