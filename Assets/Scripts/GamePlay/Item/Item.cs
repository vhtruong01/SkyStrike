using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public interface IItem : IObject
    {
        public EItem itemType { get; }
        public void Interact(ICollector collector);
    }
    public class Item : PoolableObject<ItemData>, IMagnetic, IItem
    {
        private Tweener tweener;
        public bool isMagnetic { get; private set; }
        public EItem itemType => data.metaData.type;
        private readonly Dictionary<EItemAnimationType, Tweener> animationDict = new();

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
            => StartCoroutine(HandleAffectedByExplode(vel));
        private IEnumerator HandleAffectedByExplode(Vector2 vel)
        {
            float time = 0.75f / vel.magnitude;
            float curTime = 0;
            float delta;
            Vector2 prevDir = new();
            isMagnetic = true;
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
            tweener.Restart();
        }
        private Tweener GetAnimation(EItemAnimationType type)
        {
            if (!animationDict.TryGetValue(type, out var tweener))
            {
                switch (type)
                {
                    case EItemAnimationType.Zoom:
                        tweener = transform.DOScale(1.25f * data.metaData.size, 2.5f);
                        break;
                    case EItemAnimationType.Fade:
                        tweener = spriteRenderer.DOFade(0.25f, 2);
                        break;
                    case EItemAnimationType.Rotate:
                        tweener = transform.DORotate(new(0, 0, (Random.Range(0, 2) * 2 - 1) * 360), 2.5f * Random.Range(2, 5), RotateMode.FastBeyond360).SetEase(Ease.InOutFlash);
                        break;
                }
                animationDict[type] = tweener.SetLoops(-1, LoopType.Yoyo).Pause();
            }
            return tweener;
        }
        public void Update()
        {
            if (!isActive) return;
            data.lifetime -= Time.deltaTime;
            transform.position += ItemData.dropVelocity * Time.deltaTime;
            if (data.lifetime <= 0)
                Lost();
        }
        public void HandleAffectedByGravity(Vector2 dir)
            => transform.position += dir.SetZ(transform.position.z);
        public void Interact(ICollector collector)
        {
            if (isActive)
            {
                collector.Collect(itemType);
                Disappear();
            }
        }
        private void Lost()
        {
            if (data.metaData.type != EItem.Star1 && data.metaData.type != EItem.Star5)
                SoundManager.PlaySound(ESound.LostItem);
            Disappear();
        }
        public override void Disappear()
        {
            base.Disappear();
            tweener?.Pause();
            isMagnetic = false;
        }
        public void OnDestroy()
            => tweener?.Kill();
    }
}