using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class UIGroup : MonoBehaviour
        {
            [SerializeField] private GameObject prefab;
            [SerializeField] private Color selectedColor;
            [SerializeField] private Color defaultColor;
            [SerializeField] private List<GameObject> items;
            private ObjectPool<GameObject> pool;
            private GameObject selectedItem;

            public void Awake()
            {
                pool = new(CreateNewObject);
            }
            private GameObject CreateNewObject() => Instantiate(prefab, transform, false);
            public T GetItem<T>(int index) where T : Component
            {
                //if (index >= items.Count || index < 0) return null;
                return items[index].GetComponent<T>();
            }
            public T CreateItem<T>() where T : Component
            {
                GameObject item = pool.Get();
                items.Add(item);
                Diminish(item);
                return item.GetComponent<T>();
            }
            public void RemoveItem(int index)
            {
                //if (index >= items.Count || index < 0) return;
                if (selectedItem == items[index])
                    selectedItem = null;
                pool.Release(items[index]);
                items.RemoveAt(index);
            }
            public void Clear()
            {
                for (int i = 0; i < items.Count; i++)
                    pool.Release(items[i]);
                items.Clear();
            }
            public void SelectItem<T>(T item) where T : Component
            {
                if (item == null)
                {
                    Diminish(selectedItem);
                    selectedItem = null;
                    return;
                }
                if (selectedItem == item.gameObject) return;
                Diminish(selectedItem);
                selectedItem = item.gameObject;
                Highlight(selectedItem);
            }
            public T GetSelectedItem<T>() where T : Component
            {
                return selectedItem == null ? null : selectedItem.GetComponent<T>();
            }
            private void Highlight(GameObject o) => SetBackgroundColor(o, selectedColor);
            private void Diminish(GameObject o) => SetBackgroundColor(o, defaultColor);
            private void SetBackgroundColor(GameObject o, Color color)
            {
                if (o == null) return;
                Image bg = o.GetComponent<IUIElement>()?.GetBackground();
                if (bg != null)
                    bg.color = color;
            }
        }
    }
}