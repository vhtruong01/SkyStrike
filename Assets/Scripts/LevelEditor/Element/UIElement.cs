using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIElement : MonoBehaviour, IUIElement, IObserver
        {
            [SerializeField] protected Image bg;
            public int? index { get; set; }
            public bool canRemove { get; set; }
            public IEditorData data { get; set; }
            public UnityEvent<IEditorData> onClick { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public void Init()
            {
                if (bg == null)
                    bg = GetComponent<Image>();
                onSelectUI = new();
                onClick = new();
            }
            public virtual Image GetBackground() => bg;
            public virtual void OnPointerClick(PointerEventData eventData) => SelectAndInvoke();
            public virtual void SetData(IEditorData data)
            {
                this.data = data;
                BindData();
            }
            public virtual void RemoveData()
            {
                UnbindData();
                data = null;
            }
            public virtual void InvokeData()
            {
                if (data != null || !canRemove)
                    onClick?.Invoke(data);
            }
            public virtual void SelectAndInvoke()
            {
                InvokeData();
                if (index != null)
                    onSelectUI.Invoke(index.Value);
            }
            public virtual void BindData() { }
            public virtual void UnbindData() { }
        }
    }
}