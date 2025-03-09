using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionItemUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;
            [SerializeField] private Button removeBtn;
            public ActionDataGroupObserver actionDataGroup { get; private set; }

            public override void SetData(IEditorData data)
            {
                actionDataGroup = data as ActionDataGroupObserver;
                //
            }
            public override void RemoveData() => actionDataGroup = null;
            public override IEditorData GetData() => actionDataGroup;
        }
    }
}