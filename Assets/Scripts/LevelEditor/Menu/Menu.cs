using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Menu : MonoBehaviour, IMenu
        {
            [SerializeField] protected Button collapseBtn;

            public virtual void Awake()
            {
                if (collapseBtn != null)
                    collapseBtn.onClick.AddListener(Hide);
            }
            public abstract void Init();
            public virtual void Hide() => gameObject.SetActive(false);
            public virtual void Show() => gameObject.SetActive(true);
        }
    }
}