using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public abstract class PoolableObject<T> : MonoBehaviour, IPoolableObject, IInitializable<T>
    {
        public T data { get; protected set; }
        protected SpriteRenderer spriteRenderer;
        protected BoxCollider2D col;
        public UnityAction<PoolableObject<T>> onDestroy { private get; set; }

        public virtual void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            col = GetComponentInChildren<BoxCollider2D>();
        }
        public virtual void SetData(T data)
        {
            this.data = data;
            transform.eulerAngles = Vector3.zero;
        }
        public virtual void Disappear() => onDestroy?.Invoke(this);
    }
}