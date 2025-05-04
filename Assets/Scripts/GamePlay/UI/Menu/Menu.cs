using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        [SerializeField] protected Button closeBtn;
        protected float disappearTime = 0.75f;
        protected Animator animator;

        public virtual void Awake()
        {
            if (closeBtn != null)
                closeBtn.onClick.AddListener(Collapse);
            if (obj == null)
                obj = gameObject;
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
            obj.SetActive(false);
        }
        public void Collapse() => StartCoroutine(Close());
        public void Expand() => obj.SetActive(true);
    }
}