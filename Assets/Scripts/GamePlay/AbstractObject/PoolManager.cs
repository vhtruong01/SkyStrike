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
            //material.enableInstancing = true;
        }
        protected void OnEnable()
            => Subscribe();
        protected void OnDisable()
            => Unsubscribe();
        protected virtual void Subscribe()
        {
            EventManager.Subscribe(EEventType.LoseGame, HideAll);
            EventManager.Subscribe(EEventType.WinGame, HideAll);
        }
        protected virtual void Unsubscribe()
        {
            EventManager.Unsubscribe(EEventType.LoseGame, HideAll);
            EventManager.Unsubscribe(EEventType.WinGame, HideAll);
        }
        protected virtual void DestroyItem(T item) => pool.Release(item);
        private T Create()
        {
            var item = (Instantiate(prefab, container, false).GetComponent<T>())
                ?? throw new Exception("wrong prefab type");
            item.gameObject.name = prefab.name;
            item.Init(material, i => DestroyItem(i as T));
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
        protected void HideAll()
        {
            var allChildren = container.GetComponentsInChildren<T>();
            foreach (var child in allChildren)
                child.Active(false);
        }
        protected void DestroyAll()
        {
            var allChildren = container.GetComponentsInChildren<T>();
            foreach (var child in allChildren)
                if (child.isActive)
                    child.Disappear();
        }
    }
}