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

            public override void SetData(IEditorData data)
            {
                this.data = data;
                var actionDataGroup = this.data as ActionDataGroupObserver;
                //
            }
        }
    }
}