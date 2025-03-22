using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIButton : MonoBehaviour, IUIElement
        {
            private Image bg;
            public int? index { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public void Init()
            {
                bg = gameObject.GetComponent<Image>();
                onSelectUI = new();
            }
            public Image GetBackground() => bg;
            public void SelectAndInvoke()
            {
                if (index != null)
                    onSelectUI.Invoke(index.Value);
            }
            public void OnPointerClick(PointerEventData eventData) => SelectAndInvoke();
        }
    }
}