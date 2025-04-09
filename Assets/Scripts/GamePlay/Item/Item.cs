using System;
using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Item : PoolableObject
        {
            private Animator animator;
            private ItemData itemData;
            private float elapsedTime;
            //public int quantity => itemData.quantity;
            //public EItem type => itemData.type;

            public override void Awake()
            {
                base.Awake();
                animator = GetComponent<Animator>();
            }
            public void SetData(ItemData data)
            {
                itemData = data;
                spriteRenderer.sprite = data.sprite;
                gameObject.name = data.name;
                col.size = data.sprite.bounds.size;
                animator.SetTrigger(Enum.GetName(data.animationType.GetType(), data.animationType));
                elapsedTime = 0;
                transform.localScale = Vector3.one * (data.size == 0 ? 1 : data.size);
            }
            public void Appear(Vector2 dir)
            {
                StartCoroutine(Explode(dir));
            }
            private IEnumerator Explode(Vector2 dir)
            {
                float time = 1;
                float curTime = 0;
                Vector3 pos = transform.position;
                while (curTime < time)
                {
                    transform.position = pos + (dir * Mathf.Pow(curTime / time, 0.3f)).SetZ(0);
                    yield return null;
                    curTime += Time.deltaTime;
                }
            }
            public void Update()
            {
                if (elapsedTime >= ItemData.lifeTime)
                {
                    Release();
                    return;
                }
                elapsedTime += Time.deltaTime;
                transform.position += ItemData.dropVelocity * Time.deltaTime;
            }
        }
    }
}