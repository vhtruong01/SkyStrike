using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionUI : MonoBehaviour, IUIElement
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;
            [SerializeField] private Button removeBtn;
            private Button button;
            private Image bg;
            public UnityEvent onRemove { get; private set; }
            public EnemyActionDataObserver actionData { get; private set; }
            public UnityEvent onClick { get; set; }

            public void Awake()
            {
                onClick = new();
                button = GetComponent<Button>();
                bg = GetComponent<Image>();
            }
            public void SetData(EnemyActionDataObserver actionData)
            {
                this.actionData = actionData;
                //
            }
            public void SetListener(UnityAction<ActionUI> evt)
            {
                button.onClick.AddListener(() => evt.Invoke(this));
            }

            public Image GetBackground() => bg;

            public void OnPointerClick(PointerEventData eventData) => onClick.Invoke();
        }
    }
}