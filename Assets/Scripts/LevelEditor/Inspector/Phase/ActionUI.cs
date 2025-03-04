using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionUI : UIElement 
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;
            [SerializeField] private Button removeBtn;
            private Button button;
            public EnemyActionDataObserver actionData { get; private set; }

            public override void Awake()
            {
                base.Awake();
                button = GetComponent<Button>();
            }
            public override void SetData(IData data)
            {
                actionData = data as EnemyActionDataObserver;
                //
            }
            public void SetListener(UnityAction<ActionUI> evt)
            {
                button.onClick.AddListener(() => evt.Invoke(this));
            }
        }
    }
}