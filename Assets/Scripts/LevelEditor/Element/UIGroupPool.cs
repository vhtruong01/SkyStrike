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
            public UnityAction<IData> selectDataCall { get; set; }

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
                item.onSelectUI.AddListener(SelectItem);
                if (selectDataCall != null)
                    item.onClick.AddListener(selectDataCall);
                return item;
            }
            public void CreateItem<T>(out T itemComponent) where T : Component
            {
                var item = pool.Get();
                item.index = items.Count;
                items.Add(item);
                Diminish(item);
                item.gameObject.SetActive(true);
                item.gameObject.transform.SetAsLastSibling();
                itemComponent = item.gameObject.GetComponent<T>();
            }
            public void MoveLeftSelectedItem(int amount = 1) => ChangeIndex(ref selectedItemIndex, selectedItemIndex - amount);
            public void MoveRightSelectedItem(int amount = 1) => ChangeIndex(ref selectedItemIndex, selectedItemIndex + amount);
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
                    items[i].index = i;
                oldIndex = newIndex;
            }
            public void RemoveSelectedItem() => RemoveItem(ref selectedItemIndex);
            public void RemoveItem(ref int index)
            {
                var item = GetItem(index);
                if (item == null || (!canDeselect && items.Count < 2)) return;
                ReleaseItem(index);
                for (int i = index; i < items.Count; i++)
                    items[i].index = i;
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