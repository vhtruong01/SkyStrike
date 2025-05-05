using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Editor
{
    public class UIGroup : MonoBehaviour, IInitalizable
    {
        [SerializeField] protected bool canDeselect;
        [SerializeField] protected bool useSpecificColor;
        [SerializeField] protected Color selectedColor;
        [SerializeField] protected Color defaultColor;
        private readonly List<IUIElement> items = new();
        protected int selectedItemIndex;

        public void Init()
        {
            selectedItemIndex = -1;
            if (!useSpecificColor)
            {
                selectedColor = EditorSetting.btnSelectedColor;
                defaultColor = EditorSetting.btnDefaultColor;
            }
            Preprocess();
        }
        protected virtual void Preprocess()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).TryGetComponent<IUIElement>(out var item)) continue;
                item.Init();
                item.index = items.Count;
                item.onSelectUI.AddListener(SelectItem);
                if (selectedItemIndex != item.index)
                    Diminish(item);
                items.Add(item);
            }
        }
        public void AddListener(UnityAction<int> call)
        {
            foreach (var item in items)
                item.onSelectUI.AddListener(call);
            SelectFirstItem();
        }
        public int GetSelectedItemIndex() => selectedItemIndex;
        protected void SelectFirstItem() => SelectAndInvokeItem(0);
        public void SelectNone() => SelectItem(-1);
        public void SelectAndInvokeItem(int index)
            => GetBaseItem(index)?.SelectAndInvoke();
        public virtual void SelectItem(int index)
        {
            Diminish(GetBaseItem(selectedItemIndex));
            if (canDeselect && selectedItemIndex == index) index = -1;
            selectedItemIndex = index;
            Highlight(GetBaseItem(selectedItemIndex));
        }
        public virtual IUIElement GetBaseItem(int index)
            => index < 0 || index >= items.Count ? null : items[index];
        protected void Highlight(IUIElement e)
            => SetBackgroundColor(e, selectedColor);
        protected void Diminish(IUIElement e)
            => SetBackgroundColor(e, defaultColor);
        private void SetBackgroundColor(IUIElement e, Color color)
            => e?.SetBackgroundColor(color);
    }
}