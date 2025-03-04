using System;
using UnityEngine;
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

            public override void Awake()
            {
                base.Awake();
                pool = new(CreateNewObject);
                if (!useSpecificColor)
                {
                    selectedColor = EditorSetting.btnSelectedColor;
                    defaultColor = EditorSetting.btnDefaultColor;
                }
            }
            private IUIElement CreateNewObject()
            {
                var item = Instantiate(prefab, transform, false).GetComponent<IUIElement>()
                    ?? throw new Exception("wrong prefab type");
                item.gameObject.name = prefab.name;
                return item;
            }
            public void CreateItem<T>(out T itemComponent) where T : Component
            {
                var item = pool.Get();
                int index = items.Count;
                item.onSelectUI?.AddListener(() => SelectItem(index));
                items.Add(item);
                Diminish(item);
                item.gameObject.SetActive(true);
                itemComponent = item.gameObject.GetComponent<T>();
                item.gameObject.transform.SetAsLastSibling();
            }
            public void MoveLeftSelectedItem() => ChangeIndex(ref selectedItemIndex, selectedItemIndex - 1);
            public void MoveRightSelectedItem() => ChangeIndex(ref selectedItemIndex, selectedItemIndex + 1);
            private void ReleaseItem(int index)
            {
                var item = items[index];
                ReleaseItem(item);
                item.gameObject.transform.SetAsFirstSibling();
                items.RemoveAt(index);
            }
            private void ReleaseItem(IUIElement item)
            {
                item.gameObject.SetActive(false);
                item.onSelectUI?.RemoveAllListeners();
                pool.Release(item);
            }
            private void ChangeIndex(ref int oldIndex, int newIndex)
            {
                var oldItem = GetItem(oldIndex);
                var newItem = GetItem(newIndex);
                if (oldItem == null || newItem == null || oldItem == newItem) return;
                oldItem?.gameObject.transform.SetSiblingIndex(newIndex + pool.CountInactive);
                items.RemoveAt(oldIndex);
                items.Insert(newIndex, oldItem);
                int n = Mathf.Max(oldIndex, newIndex);
                for (int i = Mathf.Min(oldIndex, newIndex); i <= n; i++)
                    ReindexItem(i);
                oldIndex = newIndex;
            }
            private void ReindexItem(int index)
            {
                items[index].onSelectUI?.RemoveAllListeners();
                items[index].onSelectUI?.AddListener(() => SelectItem(index));
            }
            public void RemoveSelectedItem() => RemoveItem(ref selectedItemIndex);
            public void RemoveItem(ref int index)
            {
                var item = GetItem(index);
                if (item == null || (!canDeselect && items.Count < 2)) return;
                ReleaseItem(index);
                item.RemoveData();
                for (int i = index; i < items.Count; i++)
                    ReindexItem(i);
                if (canDeselect)
                    index = -1;
                else
                {
                    if (index >= items.Count)
                        index -= 1;
                    SelectAndInvoke(index);
                }
            }
            public void Clear()
            {
                foreach (var item in items)
                    ReleaseItem(item);
                items.Clear();
            }
            protected override void Highlight(IUIElement e)
                => SetBackgroundColor(e, selectedColor);
            protected override void Diminish(IUIElement e)
                => SetBackgroundColor(e, defaultColor);
        }
    }
}