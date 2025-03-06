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
            public UnityEvent<IData> onClick { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public int? index { get; set; }

            public void Awake()
            {
                onSelectUI = new();
                onClick = new();
                if (bg == null)
                    bg = GetComponent<Image>();
            }
            public virtual Image GetBackground() => bg;
            public virtual void OnPointerClick(PointerEventData eventData) => Select();
            public virtual void SetData(IData data) { }
            public virtual IData GetData() => null;
            public virtual void RemoveData() { }
            public virtual void InvokeData() { }
            public virtual void Select()
            {
                if (index != null)
                    onSelectUI.Invoke(index.Value);
                IData data = GetData();
                if (data != null)
                    onClick.Invoke(data);
            }
        }
    }
}