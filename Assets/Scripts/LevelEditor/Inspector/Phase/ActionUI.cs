using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionUI : MonoBehaviour,IUIElement
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;
            [SerializeField] private Button removeBtn;
            private Button button;
            private Image bg;
            public UnityEvent onRemove {  get; private set; }
            public IEnemyActionDataObserver actionData { get; private set; }

            public void Awake()
            {
                button = GetComponent<Button>();
                bg = GetComponent<Image>();
            }
            public void SetData(IEnemyActionDataObserver actionData)
            {
                this.actionData = actionData;
                //
            }
            public void SetListener(UnityAction<ActionUI> evt)
            {
                button.onClick.AddListener(() => evt.Invoke(this));
            }

            public Image GetBackground() => bg;
        }
    }
}