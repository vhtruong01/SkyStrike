using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Game
    {
        public abstract class PoolableObject : MonoBehaviour
        {
            protected SpriteRenderer spriteRenderer;
            protected BoxCollider2D col;
            public UnityAction<PoolableObject> onDestroy { private get; set; }

            public virtual void Awake()
            {
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                col = GetComponentInChildren<BoxCollider2D>();
            }
            public virtual void Release() => onDestroy?.Invoke(this);
            public virtual void OnTriggerEnter2D(Collider2D collision) { }
        }
    }
}