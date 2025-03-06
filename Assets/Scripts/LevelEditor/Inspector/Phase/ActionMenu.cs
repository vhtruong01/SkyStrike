using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionMenu : SubMenu, IObserverMenu
        {
            public IData actionData { get; private set; }

            public abstract void BindData();
            public abstract void UnbindData();
            public override void Display(IData data)
            {
                bool isNewData = SetData(data);
                if (!CanDisplay())
                {
                    UnbindData();
                    Hide();
                    return;
                }
                if (isNewData)
                {
                    UnbindData();
                    BindData();
                }
            }
            public override bool SetData(IData data)
            {
                if (actionData == data) return false;
                actionData = data;
                return true;
            }
            public override bool CanDisplay() => actionData != null;
        }
    }
}