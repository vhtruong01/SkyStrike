using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class UIElement<T> : MonoBehaviour, IUIElement, IDragHandler, IObserver where T : class
        {
            [SerializeField] protected TextMeshProUGUI itemName;
            protected bool isDrag;
            private Image bg;
            public int? index { get; set; }
            public bool isDefault { get; set; }
            public bool isExternalCall { get; set; }
            public T data { get; set; }
            public UnityEvent<T> onClick { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public void Init()
            {
                bg = GetComponent<Image>();
                onSelectUI = new();
                onClick = new();
            }
            public virtual Image GetBackground() => bg;
            public virtual void SetData(T data)
            {
                this.data = data;
                BindData();
            }
            public virtual void RemoveData()
            {
                UnbindData();
                data = default;
            }
            public virtual void InvokeData()
            {
                if (data != null || isDefault)
                    onClick?.Invoke(data);
            }
            public void SelectAndInvoke()
            {
                InvokeData();
                if (index != null)
                    onSelectUI.Invoke(index.Value);
            }
            public virtual void SetName(string name)
            {
                if (itemName != null)
                    itemName.text = name;
            }
            public virtual T DuplicateData() => null;
            public abstract void BindData();
            public abstract void UnbindData();
            public virtual void OnDrag(PointerEventData eventData)
            {
                isDrag = true;
            }
            public virtual void OnPointerClick(PointerEventData eventData)
            {
                if (!isDrag)
                {
                    if (isExternalCall)
                        InvokeData();
                    else
                        SelectAndInvoke();
                }
                isDrag = false;
            }
        }
    }
}