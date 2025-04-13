using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class SubMenu<T> : MonoBehaviour, ISubMenu where T : IEditor
        {
            protected T data;
            public bool CanDisplay() => data != null;
            public abstract void BindData();
            public abstract void UnbindData();
            public virtual void Init() => Hide();
            public virtual void Display(T data)
            {
                if (!SetData(data)) return;
                UnbindData();
                if (!CanDisplay())
                    Hide();
                else BindData();
            }
            public virtual bool SetData(T data)
            {
                if (Equals(this.data, data)) return false;
                this.data = data;
                return true;
            }
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