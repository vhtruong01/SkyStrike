using UnityEngine;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIGroup : MonoBehaviour
        {
            private List<IUIElement> itemList;
            protected IUIElement selectedItem;
            public virtual int Count => itemList.Count;

            public virtual void Awake()
            {
                itemList = new();
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!transform.GetChild(i).TryGetComponent<IUIElement>(out var uiElement)) continue;
                    itemList.Add(uiElement);
                }
            }
            public virtual void Start()
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    int index = i;
                    itemList[index].onClick.AddListener(() => SelectItem(index));
                    if (selectedItem != itemList[index])
                        Diminish(itemList[index]);
                }
            }
            public void GetItem<T>(out T item, int index) where T : Component
            {
                item = index < 0 || index >= itemList.Count ? null : itemList[index].gameObject.GetComponent<T>();
            }
            public IUIElement GetItem(int index)
            {
                return index < 0 || index >= itemList.Count ? null : itemList[index];
            }
            protected void DeselectCurrentItem()
            {
                Diminish(selectedItem);
                selectedItem = null;
            }
            public virtual void SelectFirstItem()
            {
                SelectItem(0);
                selectedItem?.onClick.Invoke();
            }
            public void SelectItem(int index)
            {
                if (index >= 0 & index < itemList.Count)
                    SelectItem(itemList[index]);
                else SelectItem(null);
            }
            protected virtual void SelectItem(IUIElement itemObject)
            {
                //if (selectedItem == itemObject) return;
                if (itemObject == null)
                {
                    DeselectCurrentItem();
                    return;
                }
                Diminish(selectedItem);
                selectedItem = itemObject;
                Highlight(selectedItem);
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