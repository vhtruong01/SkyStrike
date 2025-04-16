using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIGroup : MonoBehaviour
        {
            [field: SerializeField] public bool canDeselect { get; set; }
            private readonly List<IUIElement> items = new();
            protected int selectedItemIndex;
            public virtual int Count => items.Count;

            public virtual void Init()
            {
                selectedItemIndex = -1;
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!transform.GetChild(i).TryGetComponent<IUIElement>(out var item)) continue;
                    item.index = items.Count;
                    item.Init();
                    item.onSelectUI.AddListener(SelectItem);
                    if (selectedItemIndex != item.index)
                        Diminish(item);
                    items.Add(item);
                }
            }
            public virtual IUIElement GetBaseItem(int index)
            {
                return index < 0 || index >= items.Count ? null : items[index];
            }
            public int GetSelectedItemIndex() => selectedItemIndex;
            public void SelectFirstItem() => SelectAndInvokeItem(0);
            public void SelectNone() => SelectItem(-1);
            public virtual void SelectAndInvokeItem(int index) 
                => GetBaseItem(index)?.SelectAndInvoke();
            public virtual void SelectItem(int index)
            {
                Diminish(GetBaseItem(selectedItemIndex));
                if (canDeselect && selectedItemIndex == index) index = -1;
                selectedItemIndex = index;
                Highlight(GetBaseItem(selectedItemIndex));
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