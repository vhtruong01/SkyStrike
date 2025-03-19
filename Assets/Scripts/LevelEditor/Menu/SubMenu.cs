using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class SubMenu : MonoBehaviour, ISubMenu
        {
            public abstract bool SetData(IEditorData data);
            public abstract void Display(IEditorData data);
            public abstract bool CanDisplay();
            public abstract void BindData();
            public abstract void UnbindData();
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