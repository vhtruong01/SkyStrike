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
            public EnemyActionDataGroupObserver actionDataGroup { get; private set; }

            public override void SetData(IData data)
            {
                actionDataGroup = data as EnemyActionDataGroupObserver;
                //
            }
            public override void RemoveData() => actionDataGroup = null;
            public override IData GetData() => actionDataGroup;
        }
    }
}