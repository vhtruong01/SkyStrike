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
            public UnityEvent<IEditorData> onClick { get; set; }
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
            public virtual void SetData(IEditorData data) { }
            public virtual IEditorData GetData() => null;
            public virtual void RemoveData() { }
            public virtual void InvokeData() { 
                IEditorData data = GetData();
                if (data != null)
                    onClick.Invoke(data);
            }
            public virtual void Select()
            {
                if (index != null)
                    onSelectUI.Invoke(index.Value);
                InvokeData();
            }
        }
    }
}