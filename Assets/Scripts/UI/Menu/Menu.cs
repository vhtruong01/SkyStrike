using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public abstract class Menu : MonoBehaviour
        {
            [SerializeField] protected Button closeBtn;
            protected float disappearTime = 4f/6;
            protected Animator animator;

            public virtual void Awake()
            {
                if (closeBtn != null)
                    closeBtn.onClick.AddListener(Collapse);
            }
            public virtual void Start()
            {
                animator = GetComponent<Animator>();     
            }
            public virtual IEnumerator Close()
            {
                if (animator != null)
                {
                    animator.SetTrigger("Close");
                    yield return new WaitForSeconds(disappearTime);
                }
                gameObject.SetActive(false);
            }
            public void Collapse() => StartCoroutine(Close());
            public void Expand() => gameObject.SetActive(true);
        }
    }
}