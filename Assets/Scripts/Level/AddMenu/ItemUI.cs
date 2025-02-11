using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class ItemUI : MonoBehaviour, IPointerClickHandler,IUIElement
        {
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
            public Image GetBackground() => bg;
            public void OnPointerClick(PointerEventData eventData)
            {
                onSelect.Invoke(this);
            }
        }
    }
}