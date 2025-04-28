using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public interface IItem : IObject
    {
        public EItem GetItemType();
        public void Interact(ICollector collector);
    }
    public class Item : PoolableObject<ItemData>, IMagnetic, IItem
    {
        private Tweener tweener;
        public bool isMagnetic { get; set; }

        public EItem GetItemType() => data.metaData.type;
        public override void Refresh()
        {
            transform.localScale = Vector3.one * (data.metaData.size == 0 ? 1 : data.metaData.size);
            transform.rotation = Quaternion.identity;
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.color = spriteRenderer.color.ChangeAlpha(1);
            col2D.size = data.metaData.sprite.bounds.size;
            spriteRenderer.sharedMaterial = data.metaData.material;
        }
        public void Appear(Vector2 vel)
        {
            isMagnetic = false;
            StartCoroutine(HandleAffectedByExplode(vel));
        }
        private IEnumerator HandleAffectedByExplode(Vector2 vel)
        {
            float time = 1f / vel.magnitude;
            float curTime = 0;
            float delta;
            Vector2 prevDir = new();
            while (curTime < time)
            {
                delta = curTime / time;
                transform.position += ((vel * delta) - prevDir).SetZ(0);
                prevDir = vel * delta;
                transform.localScale = (delta + 1) * data.metaData.size / 2 * Vector3.one;
                yield return null;
                curTime += Time.deltaTime;
            }
            transform.localScale = data.metaData.size * Vector3.one;
            tweener = GetAnimation(data.metaData.animationType);
            isMagnetic = true;
        }
        private Tweener GetAnimation(EItemAnimationType type)
        {
            Tweener tweener = null;
            switch (type)
            {
                case EItemAnimationType.Zoom:
                    tweener = transform.DOScale(1.5f * data.metaData.size, 2.5f);
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
        public void Update()
        {
            data.elapsedTime += Time.deltaTime;
            transform.position += ItemData.dropVelocity * Time.deltaTime;
            if (data.elapsedTime >= ItemData.lifeTime)
                Disappear();
        }
        public void HandleAffectedByGravity(Vector2 dir)
            => transform.position += dir.SetZ(0);
        private void OnDisable()
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
        }
        public void Interact(ICollector collector)
        {
            if (gameObject.activeSelf)
            {
                collector.Collect(GetItemType());
                Disappear();
            }
        }
    }
}