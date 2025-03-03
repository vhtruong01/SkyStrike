using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

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
            private HashSet<IUIElement> items;
            private ObjectPool<IUIElement> pool;
            public override int Count => items.Count;

            public override void Awake()
            {
                base.Awake();
                items = new();
                pool = new(CreateNewObject);
                if (!useSpecificColor)
                {
                    selectedColor = EditorSetting.btnSelectedColor;
                    defaultColor = EditorSetting.btnDefaultColor;
                }
            }
            private IUIElement CreateNewObject()
            {
                var uiElement = Instantiate(prefab, transform, false).GetComponent<IUIElement>()
                    ?? throw new Exception("wrong prefab type");
                uiElement.gameObject.name = prefab.name;
                uiElement.onClick.AddListener(() => SelectItem(uiElement));
                return uiElement;
            }
            public void CreateItem<T>(out T itemComponent) where T : Component
            {
                var item = pool.Get();
                Diminish(item);
                item.gameObject.SetActive(true);
                if (items.Count == 0)
                    selectedItem = item;
                items.Add(item);
                itemComponent = item.gameObject.GetComponent<T>();
            }
            public override void SelectFirstItem()
            {
                SelectItem(selectedItem);
                selectedItem?.onClick.Invoke();
            }
            protected override void SelectItem(IUIElement item)
            {
                if (item == null)
                {
                    Diminish(selectedItem);
                    selectedItem = null;
                    return;
                }
                if (!items.Contains(item)) return;
                Diminish(selectedItem);
                selectedItem = item;
                Highlight(selectedItem);
            }
            public void RemoveItem(GameObject item)
            {
                //if (!items.Contains(item)) return;
                //if (selectedItem == item)
                //    selectedItem = null;
                //item.SetActive(false);
                //pool.Release(item);
                //items.Remove(item);
            }
            public void Clear()
            {
                foreach (var item in items)
                {
                    item.gameObject.SetActive(false);
                    pool.Release(item);
                }
                items.Clear();
            }
            protected override void Highlight(IUIElement e)
                => SetBackgroundColor(e, selectedColor);
            protected override void Diminish(IUIElement e)
                => SetBackgroundColor(e, defaultColor);
        }
    }
}