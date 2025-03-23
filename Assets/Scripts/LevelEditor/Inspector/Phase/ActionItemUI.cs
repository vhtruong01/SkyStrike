using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionItemUI : UIElement<ActionDataObserver>
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;

            public override void BindData()
            {
            }
            public override void UnbindData()
            {
            }
            public override ActionDataObserver DuplicateData() => data.Clone();

        }
    }
}