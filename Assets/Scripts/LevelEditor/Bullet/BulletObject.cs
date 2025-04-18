using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletObject : MonoBehaviour
        {
            private SpriteRenderer spriteRenderer;
            private BulletDataObserver bulletData;
            private float elapsedTime;
            private Vector3 velocity;
            public UnityEvent<BulletObject> onDestroy { get; private set; }

            public void Awake()
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                elapsedTime = 0;
            }
            public void Init() => onDestroy = new();
            public void FixedUpdate()
            {
                elapsedTime += Time.fixedDeltaTime;
                transform.position += velocity * Time.fixedDeltaTime;
                if (elapsedTime >= bulletData.lifeTime.data)
                    onDestroy.Invoke(this);
            }
            public void Init(BulletDataObserver bulletData, Vector2 dir, Vector2 pos)
            {
                this.bulletData = bulletData;
                elapsedTime = 0;
                transform.localScale = Vector3.one * bulletData.size.data;
                velocity = (dir * bulletData.velocity.data).SetZ(0);
                transform.position = pos.SetZ(transform.position.z);
            }
        }
    }
}