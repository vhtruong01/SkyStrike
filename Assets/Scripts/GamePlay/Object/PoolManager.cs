using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.Game
{
    public abstract class PoolManager<T, K> : MonoBehaviour where T : PoolableObject<K>
    {
        [SerializeField] private bool createOtherContainer;
        [SerializeField] private PoolableObject<K> prefab;
        private Transform container;
        protected ObjectPool<T> pool;

        public virtual void Awake()
        {
            if (createOtherContainer)
            {
                GameObject obj = new(prefab.name + "Pool");
                container = obj.transform;
                container.transform.SetParent(transform.parent, false);
            }
            else container = transform;
            pool = new(Create, Get, Release);
        }
        protected virtual void DestroyItem(T item) => pool.Release(item);
        private T Create()
        {
            var item = (Instantiate(prefab, container, false).GetComponent<T>())
                ?? throw new Exception("wrong prefab type");
            item.gameObject.name = prefab.name;
            item.onDestroy = a => DestroyItem(a as T);
            return item;
        }
        private void Get(T item) => item.gameObject.SetActive(true);
        private void Release(T item) => item.gameObject.SetActive(false);
        protected T InstantiateItem(Vector3 position)
        {
            var item = pool.Get();
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