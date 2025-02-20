using System.Collections.Generic;
using System.Linq;
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
            private HashSet<GameObject> items;
            private ObjectPool<GameObject> pool;
            private GameObject selectedItem;

            public void Awake()
            {
                items = new();
                pool = new(CreateNewObject);
            }
            private GameObject CreateNewObject()
            {
                GameObject o = Instantiate(prefab, transform, false);
                o.name = prefab.name;
                return o;
            }
            public T CreateItem<T>() where T : Component
            {
                GameObject itemObject = pool.Get();
                Diminish(itemObject);
                itemObject.SetActive(true);
                items.Add(itemObject);
                return itemObject.GetComponent<T>();
            }
            public void RemoveItem(GameObject itemObject)
            {
                if (!items.Contains(itemObject)) return;
                if (selectedItem == itemObject)
                    selectedItem = null;
                itemObject.SetActive(false);
                pool.Release(itemObject);
                items.Remove(itemObject);
            }
            public void SelectItem(GameObject itemObject)
            {
                if (itemObject == null)
                {
                    Diminish(selectedItem);
                    selectedItem = null;
                    return;
                }
                if (selectedItem == itemObject || !items.Contains(itemObject)) return;
                Diminish(selectedItem);
                selectedItem = itemObject;
                Highlight(selectedItem);
            }
            public void SelectFirstItem() => SelectItem(items.First());
            public T GetSelectedItem<T>() where T : Component
            {
                return selectedItem == null ? null : selectedItem.GetComponent<T>();
            }
            public void Clear()
            {
                foreach (var item in items)
                    pool.Release(item);
                items.Clear();
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