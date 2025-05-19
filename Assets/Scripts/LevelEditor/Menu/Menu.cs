using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public abstract class Menu : MonoBehaviour, IMenu
    {
        [SerializeField] protected Button collapseBtn;

        public void Init()
        {
            if (collapseBtn != null)
                collapseBtn.onClick.AddListener(Hide);
            Preprocess();
        }
        protected abstract void Preprocess();
        public virtual void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
        public virtual void Show()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
    }
}