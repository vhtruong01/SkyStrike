using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class SubMenu : MonoBehaviour, ISubMenu
        {
            public abstract bool SetData(IData data);
            public abstract void Display(IData data);
            public abstract bool CanDisplay();
            public virtual void Hide()
            {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            public virtual void Show()
            {
                if (!gameObject.activeSelf && CanDisplay())
                    gameObject.SetActive(true);
            }
        }
    }
}