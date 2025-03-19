using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionMenu : SubMenu
        {
            public IEditorData actionData { get; private set; }

            public override void Display(IEditorData data)
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
            public override bool SetData(IEditorData data)
            {
                if (actionData == data) return false;
                actionData = data;
                return true;
            }
            public override bool CanDisplay() => actionData != null;
        }
    }
}