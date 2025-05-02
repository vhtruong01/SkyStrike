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
        protected BoxCollider2D col2D;
        protected SpriteRenderer spriteRenderer;
        protected UnityAction<PoolableObject<T>> onDestroy;
        public T data { get; private set; }
        public bool isActive { get; private set; }

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col2D = GetComponent<BoxCollider2D>();
            data = GetComponent<T>();
        }
        public abstract void Refresh();
        public void Init(Material material, UnityAction<PoolableObject<T>> onDestroy)
        {
            this.onDestroy = onDestroy;
            if (material != null)
                spriteRenderer.sharedMaterial = material;
        }
        public virtual void Disappear()
            => onDestroy?.Invoke(this);
        public void Enable(bool isEnable)
        {
            col2D.enabled = isEnable;
            spriteRenderer.enabled = isEnable;
        }
        public void Active(bool isActive)
        {
            this.isActive = isActive;
            if (gameObject.activeSelf != isActive)
                gameObject.SetActive(isActive);
        }
    }
}