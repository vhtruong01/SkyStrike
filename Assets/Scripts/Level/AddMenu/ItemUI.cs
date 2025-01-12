using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class ItemUI : MonoBehaviour, IPointerClickHandler
        {
            private static readonly Color selectedColor = new(1, 1, 1, 0.15f);
            private static readonly Color inactiveColor = new(1, 1, 1, 0);
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI text;
            [SerializeField] private Image bg;
            private EnemyMetaData _data;
            public UnityEvent<ItemUI> onSelect { get; private set; }
            public EnemyMetaData data
            {
                get => _data;
                set
                {
                    _data = value;
                    image.sprite = _data.sprite;
                    text.text = _data.type;
                    gameObject.name = _data.type;
                }
            }

            public void Awake()
            {
                onSelect = new();
                bg = GetComponent<Image>();
            }
            public void OnPointerClick(PointerEventData eventData)
            {
                onSelect.Invoke(this);
            }
            public void Active()
            {
                bg.color = selectedColor;
            }
            public void Deactive()
            {
                bg.color = inactiveColor;
            }
        }
    }
}