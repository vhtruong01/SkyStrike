using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum EItem
    {
        None = 0,
        Health,
        Star1,
        Star5,
        Shield,
        Comet,
        SingleBullet,
        DoubleBullet,
        TripleBullet,
        LaserBullet
    }
    public enum EItemAnimationType
    {
        None = 0,
        Zoom,
        Rotate,
        Fade,
    }
    public class Item : PoolableObject<ItemData>, IMagnetic
    {
        private float elapsedTime;
        private Tweener tweener;
        public bool isMagnetic { get; set; }

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
                transform.localScale = (delta + 1) * data.size / 2 * Vector3.one;
                yield return null;
                curTime += Time.deltaTime;
            }
            transform.localScale = data.size * Vector3.one;
            tweener = GetAnimation(data.animationType);
            isMagnetic = true;
        }
        private Tweener GetAnimation(EItemAnimationType type)
        {
            Tweener tweener = null;
            switch (type)
            {
                case EItemAnimationType.Zoom:
                    tweener = transform.DOScale(1.5f * data.size, 2.5f);
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
            if (elapsedTime >= ItemData.lifeTime)
            {
                Disappear();
                return;
            }
            elapsedTime += Time.deltaTime;
            transform.position += ItemData.dropVelocity * Time.deltaTime;
        }
        public void HandleAffectedByGravity(Vector2 dir) => transform.position += dir.SetZ(0);
        public void OnDisable()
            => tweener?.Kill();
    }
}