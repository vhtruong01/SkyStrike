using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public interface IPoolableObject : IObject
    {
        public void Disappear();
    }
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class PoolableObject<T> : MonoBehaviour, IPoolableObject, IRefreshable
    {
        public T data { get; set; }
        protected SpriteRenderer spriteRenderer { get; set; }
        protected BoxCollider2D col2D { get; set; }
        public UnityAction<PoolableObject<T>> onDestroy { private get; set; }

        public virtual void Awake()
        {
            col2D = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            data = GetComponent<T>();
        }
        public abstract void Refresh();
        public virtual void Disappear() => onDestroy?.Invoke(this);
    }
}