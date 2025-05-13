using SkyStrike.Game;
using System.Collections;
using UnityEngine;

namespace SkyStrike.UI
{
    public class WarningUI : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        private Coroutine coroutine;
        private Animator animator;

        private void OnEnable()
            => EventManager.Subscribe(EEventType.Warning, Show);
        private void OnDisable()
            => EventManager.Unsubscribe(EEventType.Warning, Show);
        private void Awake()
            => animator = content.GetComponent<Animator>();
        private void Show()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            content.SetActive(true);
            yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
            content.SetActive(false);
            coroutine = null;
        }
    }
}