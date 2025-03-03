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

            public virtual void Awake()
            {
                onClick = new();
                if (bg == null)
                    bg = GetComponent<Image>();
            }
            public virtual Image GetBackground() => bg;
            public virtual void OnPointerClick(PointerEventData eventData) => onClick.Invoke();
            public virtual void SetData(IData data){}
        }
    }
}