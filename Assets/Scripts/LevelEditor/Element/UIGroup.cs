using UnityEngine;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIGroup : MonoBehaviour
        {
            protected List<IUIElement> items;
            protected int selectedItemIndex;
            [field: SerializeField] public bool canDeselect { get; set; }
            public int Count => items.Count;

            public virtual void Awake()
            {
                selectedItemIndex = -1;
                items = new();
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!transform.GetChild(i).TryGetComponent<IUIElement>(out var uiElement)) continue;
                    items.Add(uiElement);
                }
            }
            public virtual void Start()
            {
                for (int i = 0; i < items.Count; i++)
                {
                    int index = i;
                    items[index].onSelectUI.AddListener(() => SelectItem(index));
                    if (selectedItemIndex != index)
                        Diminish(items[index]);
                }
            }
            public void GetItem<T>(out T item, int index) where T : Component
            {
                item = index < 0 || index >= items.Count ? null : items[index].gameObject.GetComponent<T>();
            }
            public IUIElement GetItem(int index)
            {
                return index < 0 || index >= items.Count ? null : items[index];
            }
            public IUIElement GetSelectedItem() => GetItem(selectedItemIndex);
            public void DeselectSelectedItem() => SelectItem(-1);
            public void SelectFirstItem() => SelectAndInvoke(0);
            public void SelectAndInvoke(int index)
            {
                SelectItem(index);
                GetSelectedItem()?.onClick?.Invoke();
            }
            public void SelectItem(int index)
            {
                Diminish(GetSelectedItem());
                if (canDeselect & selectedItemIndex == index) index = -1;
                selectedItemIndex = index;
                Highlight(GetSelectedItem());
            }
            protected virtual void Highlight(IUIElement e)
                => SetBackgroundColor(e, EditorSetting.btnSelectedColor);
            protected virtual void Diminish(IUIElement e)
                => SetBackgroundColor(e, EditorSetting.btnDefaultColor);
            protected void SetBackgroundColor(IUIElement e, Color color)
            {
                if (e != null)
                    e.GetBackground().color = color;
            }
        }
    }
}