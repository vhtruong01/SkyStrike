using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionMenu : MonoBehaviour, ISubMenu
        {
            [SerializeField] protected TextMeshProUGUI index;
            public string type;

            public abstract bool CanDisplay();
            public abstract bool SetData(IData data);
            public abstract void Display(IData data);
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