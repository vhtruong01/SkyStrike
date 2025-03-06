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
                    if (!transform.GetChild(i).TryGetComponent<IUIElement>(out var item)) continue;
                    items.Add(item);
                }
            }
            public virtual void Start()
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].index = i;
                    items[i].onSelectUI.AddListener(SelectItem);
                    if (selectedItemIndex != i)
                        Diminish(items[i]);
                }
            }
            public T GetSelectedItemComponent<T>() where T : Component
                => GetSelectedItem()?.gameObject.GetComponent<T>();
            public IUIElement GetItem(int index)
            {
                return index < 0 || index >= items.Count ? null : items[index];
            }
            public IUIElement GetSelectedItem() => GetItem(selectedItemIndex);
            public void DeselectSelectedItem() => SelectItem(-1);
            public void SelectFirstItem() => SelectAndInvoke(0);
            public void SelectAndInvoke(int index) => GetItem(index)?.Select();
            protected void SelectItem(int index)
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