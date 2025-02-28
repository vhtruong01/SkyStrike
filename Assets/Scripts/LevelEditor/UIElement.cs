using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIElement : MonoBehaviour, IUIElement
        {
            [SerializeField] private Image image;
            public UnityEvent onClick { get; set; }

            public void Awake()
            {
                onClick = new();
                if (image == null)
                    image = GetComponent<Image>();
            }
            public Image GetBackground() => image;
            public void OnPointerClick(PointerEventData eventData) => onClick.Invoke();
        }
    }
}