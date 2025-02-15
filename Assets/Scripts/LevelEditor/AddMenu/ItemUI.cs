using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class ItemUI : MonoBehaviour, IPointerDownHandler, IUIElement
        {
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI text;
            [SerializeField] private Image bg;
            public EnemyDataObserver data { get; set; }
            public UnityEvent<ItemUI> onSelect { get; private set; }

            public void Awake()
            {
                onSelect = new();
                bg = GetComponent<Image>();
            }
            public Image GetBackground() => bg;
            public void OnPointerDown(PointerEventData eventData)
            {
                onSelect.Invoke(this);
            }
        }
    }
}