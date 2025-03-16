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
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].index = i;
                    items[i].Init();
                    items[i].onSelectUI.AddListener(SelectItem);
                    if (selectedItemIndex != i)
                        Diminish(items[i]);
                }
            }
            public virtual IUIElement GetItem(int index)
            {
                return index < 0 || index >= items.Count ? null : items[index];
            }
            public bool TryGetValidSelectedIndex(out int index)
            {
                index = selectedItemIndex;
                return index >= 0 & index < items.Count;
            }
            public IUIElement GetSelectedItem() => GetItem(selectedItemIndex);
            public void SelectFirstItem() => SelectAndInvokeItem(0);
            public void SelectAndInvokeItem(int index) => GetItem(index)?.SelectAndInvoke();
            public void SelectNone() => SelectItem(-1);
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