using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Menu : MonoBehaviour
        {
            [SerializeField] protected Button collapseBtn;

            public virtual void Awake()
            {
                if (collapseBtn != null)
                    collapseBtn.onClick.AddListener(Collapse);
            }
            public abstract void Init();
            public virtual void Collapse() => gameObject.SetActive(false);
            public virtual void Expand() => gameObject.SetActive(true); 
        }
    }
}