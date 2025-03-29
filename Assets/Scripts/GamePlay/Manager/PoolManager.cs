using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Game
    {
        public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour
        {
            [SerializeField] private GameObject prefab;
            private ObjectPool<T> pool;

            public virtual void Awake()
            {
                pool = new(CreateObject);
            }
            private T CreateObject()
            {
                var item = (Instantiate(prefab, transform, false).GetComponent<T>())
                    ?? throw new Exception("wrong prefab type");
                item.gameObject.name = prefab.name;
                return item;
            }
            protected T CreateItem()
            {
                var item = pool.Get();
                item.gameObject.SetActive(true);
                return item;
            }
            public void RemoveItem(T item)
            {
                item.gameObject.SetActive(false);
                pool.Release(item);
            }
            public void Clear()
            {
                var allChildren = gameObject.GetComponentsInChildren<T>();
                foreach (var child in allChildren)
                {
                    if (gameObject.activeSelf)
                        pool.Release(child);
                }
            }
        }
    }
}