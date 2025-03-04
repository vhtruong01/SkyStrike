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
            [SerializeField] protected Image bg;
            public UnityEvent onClick { get; set; }
            public UnityEvent onSelectUI { get; set; }

            public virtual void Awake()
            {
                onSelectUI = new();
                onClick = new();
                if (bg == null)
                    bg = GetComponent<Image>();
            }
            public virtual Image GetBackground() => bg;
            public virtual void OnPointerClick(PointerEventData eventData)
            {
                onSelectUI.Invoke();
                onClick.Invoke();
            }
            public virtual void SetData(IData data){}
        }
    }
}