using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionMenu : MonoBehaviour, ISubMenu, IObserverMenu
        {
            public string type;
            [SerializeField] protected TextMeshProUGUI index;
            protected IData actionData;

            public abstract void BindData();
            public abstract void UnbindData();
            public virtual void Display(IData data)
            {
                bool isNewData = SetData(data);
                if (isNewData)
                {
                    UnbindData();
                    if (CanDisplay())
                        BindData();
                    else
                    {
                        Hide();
                        return;
                    }
                }
                Show();
            }
            public virtual bool SetData(IData data)
            {
                if (actionData == data) return false;
                actionData = data;
                return true;
            }
            public virtual bool CanDisplay() => actionData != null;
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