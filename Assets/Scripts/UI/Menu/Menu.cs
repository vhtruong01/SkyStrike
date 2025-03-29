using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public abstract class Menu : MonoBehaviour
        {
            [SerializeField] protected Button closeBtn;
            private float disappearTime = 1;
            protected Animator animator;
            public UnityAction closeAction { protected get; set; }

            public virtual void Awake()
            {
                if (closeBtn != null)
                    closeBtn.onClick.AddListener(() => StartCoroutine(Close()));
                animator = GetComponent<Animator>();
            }
            public virtual IEnumerator Close()
            {
                closeAction?.Invoke();
                if (animator != null)
                {
                    animator.SetTrigger("Close");
                    yield return new WaitForSeconds(disappearTime);
                }
                gameObject.SetActive(false);
            }
        }
    }
}