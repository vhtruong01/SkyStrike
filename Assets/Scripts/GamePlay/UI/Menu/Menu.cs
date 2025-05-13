using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected Button closeBtn;
        protected Animator animator;

        public virtual void Awake()
        {
            if (closeBtn != null)
                closeBtn.onClick.AddListener(Collapse);
        }
        public virtual void Start()
            => animator = GetComponentInChildren<Animator>();
        public virtual IEnumerator Close()
        {
            if (animator != null)
            {
                animator.SetTrigger("Close");
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            }
            gameObject.SetActive(false);
        }
        public virtual void Collapse() => StartCoroutine(Close());
        public virtual void Expand() => gameObject.SetActive(true);
    }
}