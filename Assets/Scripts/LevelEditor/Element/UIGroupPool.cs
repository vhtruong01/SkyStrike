using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace SkyStrike.Editor
{
    public abstract class UIGroupPool<T> : UIGroup where T : IData
    {
        [SerializeField] protected string defaultName;
        [SerializeField] protected GameObject prefab;
        [SerializeField] protected Transform containerTransform;
        protected List<UIElement<T>> items;
        protected ObjectPool<UIElement<T>> pool;
        protected UnityAction<T> selectDataCall;
        protected UnityAction<T> deselectDataCall;
        protected IDataList<T> dataList;
        private IScalableScreen screen;

        protected override void Preprocess()
        {
            screen = GetComponentInChildren<IScalableScreen>(true);
            items = new();
            pool = new(CreateObject);
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
        public void Init(UnityAction<T> selectCall, UnityAction<T> deselectCall = null)
        {
            selectDataCall = selectCall;
            deselectDataCall = deselectCall;
        }
        protected virtual UIElement<T> CreateObject()
        {
            var item = Instantiate(prefab, containerTransform, false).GetComponent<UIElement<T>>()
                ?? throw new Exception("wrong prefab type");
            item.gameObject.name = prefab.name;
            if (screen != null)
                item.screen = screen;
            item.isDefault = false;
            item.Init();
            item.onSelectUI.AddListener(SelectItem);
            item.onClick.AddListener(InvokeData);
            return item;
        }
        private void InvokeData(T data)
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
        private void ReleaseItem(int index)
        {
            dataList?.Remove(index, out _);
            var item = items[index];
            items.RemoveAt(index);
            ReleaseItem(item);
            item.gameObject.transform.SetAsFirstSibling();
        }
        private void ReleaseItem(UIElement<T> item)
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
            if (dataList != null)
                for (int i = 0; i < items.Count; i++)
                    dataList.Set(i, items[i].data);
        }
        public UIElement<T> GetItem(int index)
            => index < 0 || index >= items.Count ? null : items[index];
        public override IUIElement GetBaseItem(int index) => GetItem(index);
        public UIElement<T> GetSelectedItem() => GetItem(selectedItemIndex);
        public bool RemoveSelectedItem()
            => RemoveItem(selectedItemIndex);
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
        public void Clear()
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
        public void RefreshDataList() => DisplayDataList(dataList);
        public void DisplayDataList(IDataList<T> dataList)
        {
            Clear();
            this.dataList = dataList;
            if (dataList == null) return;
            dataList.GetList(out var lst);
            foreach (var data in lst)
                CreateItem(data);
            //if (!canDeselect) SelectFirstItem();
        }
        public UIElement<T> CreateItemAndAddData(T data)
        {
            if (dataList != null)
            {
                if (data != null)
                    dataList.Add(data);
                else dataList.CreateEmpty(out data);
            }
            return CreateItem(data);
        }
        public void CreateEmptyItem()
            => CreateItemAndAddData(default);
        public void DuplicateSelectedItem()
        {
            var selectedItem = GetSelectedItem();
            if (selectedItem != null)
            {
                var itemData = GetSelectedItem().DuplicateData();
                if (itemData != null)
                    CreateItemAndAddData(itemData);
            }
        }
        public void MoveLeftSelectedItem()
        {
            if (items.Count <= 1 || selectedItemIndex == -1) return;
            if (selectedItemIndex - 1 >= 0)
            {
                SwapItem(selectedItemIndex, selectedItemIndex - 1);
                selectedItemIndex -= 1;
            }
            else MoveItemArray(ref selectedItemIndex, -1);
        }
        public void MoveRightSelectedItem()
        {
            if (items.Count <= 1 || selectedItemIndex == -1) return;
            if (selectedItemIndex + 1 < items.Count)
            {
                SwapItem(selectedItemIndex, selectedItemIndex + 1);
                selectedItemIndex += 1;
            }
            else MoveItemArray(ref selectedItemIndex, 0);
        }
        private void SwapItem(int i1, int i2)
        {
            if (i1 > i2)
                (i1, i2) = (i2, i1);
            var item1 = GetItem(i1);
            var item2 = GetItem(i2);
            item1.gameObject.transform.SetSiblingIndex(i2 + pool.CountInactive);
            item2.gameObject.transform.SetSiblingIndex(i1 + pool.CountInactive);
            (items[i2], items[i1]) = (items[i1], items[i2]);
            items[i2].index = i2;
            items[i1].index = i1;
            if (dataList != null)
            {
                dataList.GetList(out var list);
                list.Swap(i1, i2);
            }
        }
    }
}