using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.Game
{
    public abstract class PoolManager<T, K> : MonoBehaviour where T : PoolableObject<K> where K : IGame
    {
        [SerializeField] private PoolableObject<K> prefab;
        [SerializeField] private Transform container;
        protected ObjectPool<T> pool;

        public virtual void Awake()
        {
            if (container == null)
                container = transform;
            pool = new(Create, Get, Release);
        }
        protected virtual void DestroyItem(T item) => pool.Release(item);
        protected virtual T Create()
        {
            var item = (Instantiate(prefab, container, false).GetComponent<T>())
                ?? throw new Exception("wrong prefab type");
            item.gameObject.name = prefab.name;
            item.onDestroy = a => DestroyItem(a as T);
            return item;
        }
        protected virtual void Get(T item) => item.gameObject.SetActive(true);
        protected virtual void Release(T item) => item.gameObject.SetActive(false);
        public virtual T InstantiateItem(K data, Vector3 position)
        {
            var item = pool.Get();
            item.SetData(data);
            item.transform.localScale = Vector3.one;
            item.transform.position = position;
            return item;
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