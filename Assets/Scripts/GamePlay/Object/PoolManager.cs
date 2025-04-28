using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.Game
{
    public abstract class PoolManager<T, K> : MonoBehaviour where T : PoolableObject<K>
    {
        [SerializeField] protected bool createOtherContainer;
        [SerializeField] protected PoolableObject<K> prefab;
        private Transform container;
        protected ObjectPool<T> pool;
        private Material material;

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
            material = prefab.GetComponent<SpriteRenderer>().sharedMaterial;
        }
        protected virtual void DestroyItem(T item) => pool.Release(item);
        private T Create()
        {
            var item = (Instantiate(prefab, container, false).GetComponent<T>())
                ?? throw new Exception("wrong prefab type");
            item.gameObject.name = prefab.name;
            item.SetMaterial(material);
            item.onDestroy = a => DestroyItem(a as T);
            return item;
        }
        private void Get(T item) => item.Active(true);
        private void Release(T item) => item.Active(false);
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