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
            spriteRenderer = GetComponent<SpriteRenderer>();
            col2D = GetComponent<BoxCollider2D>();
            data = GetComponent<T>();
        }
        public abstract void Refresh();
        public void SetMaterial(Material material)
            => spriteRenderer.sharedMaterial = material;
        public virtual void Disappear()
            => onDestroy?.Invoke(this);
        public void Enable(bool isEnable)
        {
            col2D.enabled = isEnable;
            spriteRenderer.enabled = isEnable;
        }
        public void Active(bool isActive)
        {
            if (gameObject.activeSelf != isActive)
                gameObject.SetActive(isActive);
        }
    }
}