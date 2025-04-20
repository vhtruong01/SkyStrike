using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class Item : PoolableObject<ItemData>, IPullable
    {
        private float elapsedTime;
        private Tweener tweener;

        public EItem GetItemType() => data.type;
        public override void SetData(ItemData data)
        {
            base.SetData(data);
            transform.localScale = Vector3.one * (data.size == 0 ? 1 : data.size);
            transform.eulerAngles = Vector3.zero;
            spriteRenderer.sprite = data.sprite;
            spriteRenderer.material = data.material;
            spriteRenderer.color = spriteRenderer.color.ChangeAlpha(1);
            col.size = data.sprite.bounds.size;
            elapsedTime = 0;
        }
        public void Appear(Vector2 vel)
            => StartCoroutine(HandleAffectedByExplode(vel));
        private IEnumerator HandleAffectedByExplode(Vector2 vel)
        {
            float time = 1;
            float curTime = 0;
            float delta;
            Vector2 curDir = new();
            while (curTime < time)
            {
                delta = curTime / time;
                transform.position += ((vel * delta) - curDir).SetZ(0);
                curDir = vel * delta;
                transform.localScale = (delta + 1) * data.size / 2 * Vector3.one;
                yield return null;
                curTime += Time.deltaTime;
            }
            transform.localScale = data.size * Vector3.one;
            tweener = GetAnimation(data.animationType);
        }
        private Tweener GetAnimation(EItemAnimationType type)
        {
            Tweener tweener = null;
            switch (type)
            {
                case EItemAnimationType.Zoom:
                    tweener = transform.DOScale(0.75f, 5);
                    break;
                case EItemAnimationType.Fade:
                    tweener = spriteRenderer.DOFade(0.5f, 1);
                    break;
                case EItemAnimationType.Rotate:
                    tweener = transform.DORotate(new(0, 0, 360), 10, RotateMode.FastBeyond360).SetEase(Ease.InOutFlash);
                    break;
            }
            return tweener.SetLoops(-1, LoopType.Yoyo);
        }
        public override void Disappear()
        {
            tweener?.Kill();
            base.Disappear();
        }
        public void Update()
        {
            if (elapsedTime >= ItemData.lifeTime)
            {
                Disappear();
                return;
            }
            elapsedTime += Time.deltaTime;
            transform.position += ItemData.dropVelocity * Time.deltaTime;
        }
        public void HandleAffectedByGravity(Vector2 dir) => transform.position += dir.SetZ(0);
    }
}