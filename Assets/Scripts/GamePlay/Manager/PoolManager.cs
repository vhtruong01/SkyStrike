using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Game
    {
        public abstract class PoolManager<T> : MonoBehaviour where T : PoolableObject
        {
            [SerializeField] private PoolableObject prefab;
            protected ObjectPool<T> pool;

            public virtual void Awake() => pool = new(Create, Get, Release);
            protected virtual T Create()
            {
                var item = (Instantiate(prefab, transform, false).GetComponent<T>())
                    ?? throw new Exception("wrong prefab type");
                item.gameObject.name = prefab.name;
                item.onDestroy = a => DestroyItem(a as T);
                return item;
            }
            protected virtual void Get(T item) => item.gameObject.SetActive(true);
            protected virtual void Release(T item) => item.gameObject.SetActive(false);
            protected virtual T InstantiateItem()
            {
                var item = pool.Get();
                item.transform.localScale = Vector3.one;
                return item;
            }
            protected virtual void DestroyItem(T item) => pool.Release(item);
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