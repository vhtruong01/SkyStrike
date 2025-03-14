using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIGroupPool : UIGroup
        {
            [SerializeField] private bool useSpecificColor;
            [SerializeField] protected Color selectedColor;
            [SerializeField] protected Color defaultColor;
            [SerializeField] private GameObject prefab;
            private ObjectPool<IUIElement> pool;
            public UnityAction<IEditorData> selectDataCall { get; set; }

            public override void Awake()
            {
                base.Awake();
                pool = new(CreateObject);
                if (!useSpecificColor)
                {
                    selectedColor = EditorSetting.btnSelectedColor;
                    defaultColor = EditorSetting.btnDefaultColor;
                }
            }
            public override void Init() { }
            private IUIElement CreateObject()
            {
                var item = Instantiate(prefab, transform, false).GetComponent<IUIElement>()
                    ?? throw new Exception("wrong prefab type");
                item.gameObject.name = prefab.name;
                item.Init();
                item.onSelectUI.AddListener(SelectItem);
                item.onClick.AddListener(InvokeData);
                return item;
            }
            public void InvokeData(IEditorData data)
            {
                if (canDeselect & data == GetSelectedItem()?.data)
                    selectDataCall?.Invoke(null);
                else
                    selectDataCall?.Invoke(data);
            }
            public IUIElement CreateItem(IEditorData data)
            {
                var item = pool.Get();
                item.index = items.Count;
                item.SetData(data);
                items.Add(item);
                Diminish(item);
                item.gameObject.SetActive(true);
                item.gameObject.transform.SetAsLastSibling();
                return item;
            }
            public void CreateItem<T>(out T itemComponent, IEditorData data) where T : Component
            {
                var item = CreateItem(data);
                itemComponent = item.gameObject.GetComponent<T>();
            }
            public T GetSelectedItemComponent<T>() where T : Component
                => GetSelectedItem()?.gameObject.GetComponent<T>();
            public int GetItemIndex(IEditorData data)
            {
                for (int i = 0; i < items.Count; i++)
                    if (items[i].data == data)
                        return i;
                return -1;
            }
            public IUIElement GetItem(IEditorData data)
            {
                for (int i = 0; i < items.Count; i++)
                    if (items[i].data == data)
                        return items[i];
                return null;
            }
            public void SelectItem(IEditorData data)
            {
                if (data != null)
                    SelectItem(GetItemIndex(data));
                else if (canDeselect) SelectNone();
            }
            public void MoveLeftSelectedItem(int amount = 1) => MoveItemByIndex(selectedItemIndex, selectedItemIndex - amount);
            public void MoveRightSelectedItem(int amount = 1) => MoveItemByIndex(selectedItemIndex, selectedItemIndex + amount);
            private void ReleaseItem(int index)
            {
                var item = items[index];
                ReleaseItem(item);
                item.gameObject.transform.SetAsFirstSibling();
                items.RemoveAt(index);
            }
            private void ReleaseItem(IUIElement item)
            {
                item.index = null;
                item.gameObject.SetActive(false);
                pool.Release(item);
                item.RemoveData();
            }
            public void MoveItemByIndex(int oldIndex, int newIndex)
            {
                var oldItem = GetItem(oldIndex);
                var newItem = GetItem(newIndex);
                if (oldItem == null || newItem == null || oldItem == newItem) return;
                oldItem?.gameObject.transform.SetSiblingIndex(newIndex + pool.CountInactive);
                items.RemoveAt(oldIndex);
                items.Insert(newIndex, oldItem);
                int n = Mathf.Max(oldIndex, newIndex);
                for (int i = Mathf.Min(oldIndex, newIndex); i <= n; i++)
                    items[i].index = i;
                if (selectedItemIndex == oldIndex)
                    selectedItemIndex = newIndex;
            }
            public void RemoveSelectedItem() => RemoveItem(selectedItemIndex);
            public void RemoveItem(IEditorData data) => RemoveItem(GetItemIndex(data));
            public void RemoveItem(int index)
            {
                var item = GetItem(index);
                if (item == null || (!canDeselect && items.Count < 2)) return;
                ReleaseItem(index);
                for (int i = index; i < items.Count; i++)
                    items[i].index = i;
                if (index == selectedItemIndex)
                {
                    if (canDeselect)
                        selectedItemIndex = -1;
                    else
                    {
                        if (index >= items.Count)
                            selectedItemIndex -= 1;
                        SelectAndInvokeItem(selectedItemIndex);
                    }
                }
            }
            public void Clear()
            {
                SelectNone();
                for (int i = 0; i < items.Count; i++)
                    ReleaseItem(items[i]);
                items.Clear();
            }
            protected override void Highlight(IUIElement e)
                => SetBackgroundColor(e, selectedColor);
            protected override void Diminish(IUIElement e)
                => SetBackgroundColor(e, defaultColor);
        }
    }
}