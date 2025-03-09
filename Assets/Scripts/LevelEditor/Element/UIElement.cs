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
            public IEditorData data { get; set; }
            public UnityEvent<IEditorData> onClick { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public int? index { get; set; }
            public void Init()
            {
                if (bg == null)
                    bg = GetComponent<Image>();
                onSelectUI = new();
                onClick = new();
            }
            public virtual void SetData(IEditorData data) { }
            public virtual Image GetBackground() => bg;
            public virtual void OnPointerClick(PointerEventData eventData) => Select();
            public void RemoveData() => data = null;
            public virtual void InvokeData()
            {
                if (data != null)
                    onClick.Invoke(data);
            }
            public virtual void Select()
            {
                InvokeData();
                if (index != null)
                    onSelectUI.Invoke(index.Value);
            }
        }
    }
}