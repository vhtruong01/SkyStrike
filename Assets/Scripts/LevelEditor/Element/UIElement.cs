using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public abstract class UIElement<T> : MonoBehaviour, IPointerClickHandler, IUIElement, IObserver where T : IData
    {
        [SerializeField] protected TextMeshProUGUI itemName;
        protected Image bg;
        public int? index { get; set; }
        public bool isDefault { get; set; }
        public T data { get; set; }
        public UnityEvent<T> onClick { get; set; }
        public UnityEvent<int> onSelectUI { get; set; }
        public IScalableScreen screen { protected get; set; }

        public virtual void Init()
        {
            bg = GetComponent<Image>();
            onSelectUI = new();
            onClick = new();
        }
        public void SetBackgroundColor(Color c)
            => bg.color = c;
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
        public void InvokeData()
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
        public virtual void Click() => SelectAndInvoke();
        public virtual void SetName(string name)
        {
            if (itemName != null)
                itemName.text = name;
        }
        public virtual T DuplicateData() => default;
        public abstract void BindData();
        public abstract void UnbindData();
        public virtual void OnPointerClick(PointerEventData eventData) => Click();
    }
}