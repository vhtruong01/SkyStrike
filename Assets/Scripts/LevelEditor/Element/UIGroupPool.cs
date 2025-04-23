using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace SkyStrike.Editor
{
    public abstract class UIGroupPool<T> : UIGroup where T : IData
    {
        [SerializeField] protected bool useSpecificColor;
        [SerializeField] protected Color selectedColor;
        [SerializeField] protected Color defaultColor;
        [SerializeField] protected string defaultName;
        [SerializeField] protected GameObject prefab;
        [SerializeField] protected Transform containerTransform;
        protected List<UIElement<T>> items;
        protected ObjectPool<UIElement<T>> pool;
        protected UnityAction<T> selectDataCall;
        protected UnityAction<T> deselectDataCall;
        public override int Count => items.Count;

        public override void Init()
        {
            selectedItemIndex = -1;
            items = new();
            pool = new(CreateObject);
            if (!useSpecificColor)
            {
                selectedColor = EditorSetting.btnSelectedColor;
                defaultColor = EditorSetting.btnDefaultColor;
            }
            for (int i = 0; i < containerTransform.childCount; i++)
            {
                if (!containerTransform.GetChild(i).TryGetComponent<UIElement<T>>(out var item)) continue;
                item.isDefault = true;
                item.index = items.Count;
                item.Init();
                item.onSelectUI.AddListener(SelectItem);
                item.onClick.AddListener(InvokeData);
                if (selectedItemIndex != item.index)
                    Diminish(item);
                items.Add(item);
            }
        }
        public virtual void Init(UnityAction<T> selectCall, UnityAction<T> deselectCall = null)
        {
            selectDataCall = selectCall;
            deselectDataCall = deselectCall;
        }
        protected virtual UIElement<T> CreateObject()
        {
            var item = Instantiate(prefab, containerTransform, false).GetComponent<UIElement<T>>()
                ?? throw new Exception("wrong prefab type");
            item.gameObject.name = prefab.name;
            item.isDefault = false;
            item.Init();
            item.onSelectUI.AddListener(SelectItem);
            item.onClick.AddListener(InvokeData);
            return item;
        }
        protected void InvokeData(T data)
        {
            if (selectDataCall == null) return;
            var item = GetSelectedItem();
            if (canDeselect && item != null && Equals(data, item.data))
                selectDataCall.Invoke(default);
            else
                selectDataCall.Invoke(data);
        }
        public virtual UIElement<T> CreateItem(T data)
        {
            if (data == null)
                throw new Exception("data must not be null");
            var item = pool.Get();
            item.SetData(data);
            item.index = items.Count;
            items.Add(item);
            if (!string.IsNullOrEmpty(defaultName))
                item.SetName("New " + defaultName + " " + items.Count);
            Diminish(item);
            item.gameObject.SetActive(true);
            item.gameObject.transform.SetAsLastSibling();
            return item;
        }
        public int GetItemIndex(T data)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (Equals(data, items[i].data))
                    return i;
            }
            return -1;
        }
        public UIElement<T> GetItem(T data) => GetItem(GetItemIndex(data));
        public void SelectItem(T data) => SelectItem(GetItemIndex(data));
        public override void SelectItem(int index)
        {
            T prevData = (index == selectedItemIndex && index != -1) ? GetSelectedItem().data : default;
            base.SelectItem(index);
            if (prevData != null)
                deselectDataCall?.Invoke(prevData);
        }
        public void SelectAndInvokeItem(T data)
            => SelectAndInvokeItem(GetItemIndex(data));
        protected virtual void ReleaseItem(int index)
        {
            var item = items[index];
            items.RemoveAt(index);
            ReleaseItem(item);
            item.gameObject.transform.SetAsFirstSibling();
        }
        protected void ReleaseItem(UIElement<T> item)
        {
            item.index = null;
            item.gameObject.SetActive(false);
            pool.Release(item);
            item.RemoveData();
        }
        public virtual void MoveItemArray(ref int startIndex, int newIndex, int len = 1)
        {
            //print(startIndex + " " + newIndex + " " + len);
            if (startIndex == newIndex || len < 1) return;
            UIElement<T>[] itemArr = new UIElement<T>[len];
            for (int i = 0; i < len; i++)
                itemArr[i] = GetItem(i + startIndex);
            if (startIndex > newIndex && newIndex >= 0)
            {
                for (int i = startIndex + len - 1; i >= newIndex + len; i--)
                {
                    items[i] = items[i - len];
                    items[i].index = i;
                }
                for (int i = newIndex + len - 1; i >= newIndex; i--)
                {
                    items[i] = itemArr[i - newIndex];
                    items[i].index = i;
                    items[i].gameObject.transform.SetSiblingIndex(newIndex + pool.CountInactive);
                }
            }
            else
            {
                if (newIndex < 0) newIndex = items.Count - 1;
                for (int i = startIndex; i <= newIndex - len; i++)
                {
                    items[i] = items[i + len];
                    items[i].index = i;
                }
                for (int i = newIndex - len + 1; i <= newIndex; i++)
                {
                    items[i] = itemArr[i - newIndex + len - 1];
                    items[i].index = i;
                    items[i].gameObject.transform.SetSiblingIndex(newIndex + pool.CountInactive);
                }
            }
            startIndex = itemArr[0].index.Value;
        }
        public UIElement<T> GetItem(int index)
        {
            return index < 0 || index >= items.Count ? null : items[index];
        }
        public override IUIElement GetBaseItem(int index) => GetItem(index);
        public UIElement<T> GetSelectedItem() => GetItem(selectedItemIndex);
        public bool TryGetValidSelectedIndex(out int index)
        {
            index = selectedItemIndex;
            return index >= 0 & index < items.Count;
        }
        public void RemoveItem(T data) => RemoveItem(GetItemIndex(data));
        public bool RemoveItem(int index)
        {
            var item = GetItem(index);
            if (item == null || (!canDeselect && items.Count < 2) || item.isDefault) return false;
            if (index == selectedItemIndex)
            {
                if (canDeselect)
                    SelectNone();
                else
                {
                    if (index != items.Count - 1)
                    {
                        SelectAndInvokeItem(index + 1);
                        selectedItemIndex = index;
                    }
                    else SelectAndInvokeItem(index - 1);
                }
            }
            for (int i = index + 1; i < items.Count; i++)
                items[i].index = i - 1;
            ReleaseItem(index);
            return true;
        }
        public virtual void Clear()
        {
            if (items == null || items.Count == 0) return;
            List<UIElement<T>> unremovedItem = new();
            SelectNone();
            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].isDefault)
                    ReleaseItem(items[i]);
                else unremovedItem.Add(items[i]);
            }
            items = unremovedItem;
        }
        protected override void Highlight(IUIElement e)
            => SetBackgroundColor(e, selectedColor);
        protected override void Diminish(IUIElement e)
            => SetBackgroundColor(e, defaultColor);
    }
}