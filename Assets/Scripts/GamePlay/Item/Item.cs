using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Item : PoolableObject<ItemData>, IPullable
        {
            private ItemData data;
            private float elapsedTime;
            private Tweener tweener;

            public EItem GetItemType() => data.type;
            public override void SetData(ItemData data)
            {
                this.data = data;
                transform.localScale = Vector3.one * (data.size == 0 ? 1 : data.size);
                spriteRenderer.sprite = data.sprite;
                spriteRenderer.material = data.material;
                col.size = data.sprite.bounds.size;
                elapsedTime = 0;
                transform.eulerAngles = Vector3.zero;
            }
            public void Appear(Vector2 dir)
                => StartCoroutine(HandleAffectedByExplode(dir));
            private IEnumerator HandleAffectedByExplode(Vector2 dir)
            {
                float time = 1;
                float curTime = 0;
                float delta;
                Vector2 curDir = new();
                while (curTime < time)
                {
                    delta = Mathf.Sqrt(curTime / time);
                    transform.position += ((dir * delta) - curDir).SetZ(0);
                    curDir = dir * delta;
                    transform.localScale = (delta + 1) * data.size / 2 * Vector3.one;
                    yield return null;
                    curTime += Time.deltaTime;
                }
                transform.localScale = data.size * Vector3.one;
                /// item animation
                //tweener = transform.DORotate(new(0, 0, 360), 5, RotateMode.FastBeyond360)
                //        .SetEase(Ease.InOutFlash)
                //        .SetLoops(-1);
            }
            public override void Release()
            {
                tweener?.Kill();
                base.Release();
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
            public void HandleAffectedByGravity(Vector2 dir) => transform.position += dir.SetZ(0);
        }
    }
}