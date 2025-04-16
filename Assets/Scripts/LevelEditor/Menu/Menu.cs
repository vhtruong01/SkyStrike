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
            public virtual void Restore()
            {
                if (PlayerPrefs.GetInt(GetType().Name, 0) == 0)
                    Hide();
                else Show();
            }
            public virtual void SaveSetting()
            {
                PlayerPrefs.SetInt(GetType().Name, gameObject.activeSelf ? 1 : 0);
            }
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
}