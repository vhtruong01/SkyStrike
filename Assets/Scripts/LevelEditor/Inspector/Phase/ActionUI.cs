using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionUI : MonoBehaviour
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;
            [SerializeField] private Button removeBtn;
            public UnityEvent onRemove {  get; private set; }
            public IEnemyActionDataObserver actionData { get; private set; }
            public EActionType type { get; private set; }
            private Button button;
            public void Awake()
            {
                button = GetComponent<Button>();
            }

            public void Display(IEnemyActionDataObserver actionData, EActionType type)
            {
                this.actionData = actionData;
                this.type = type;
                //
            }
            public void SetListener(UnityAction<ActionUI> evt)
            {
                button.onClick.AddListener(() => evt.Invoke(this));
            }
        }
    }
}