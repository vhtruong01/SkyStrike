using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class SpriteChangeAnimation : EnemyAnimation
    {
        protected int curIndex;
        protected SpriteRenderer spriteRenderer;
        protected List<Sprite> sprites;

        public SpriteChangeAnimation(SpriteRenderer spriteRenderer, float interval)
        {
            this.interval = interval;
            this.spriteRenderer = spriteRenderer;
        }
        public void SetData(List<Sprite> sprites)
            => this.sprites = sprites;
        public override IEntityAnimation Reset()
        {
            elapedTime = 0;
            curIndex = 0;
            return this;
        }
        public override IEntityAnimation SetTotalTime(float totalTime)
        {
            base.SetTotalTime(totalTime);
            if (sprites.Count > 0)
                interval = totalTime / sprites.Count;
            return this;
        }
        public override void Animate(float deltaTime)
        {
            //dir
            if (!isActive || sprites.Count == 0) return;
            elapedTime += deltaTime;
            if (elapedTime < curIndex * interval) return;
            spriteRenderer.sprite = sprites[curIndex];
            curIndex++;
            if (curIndex >= sprites.Count)
            {
                if (isLoop) Reset();
                else
                {
                    isActive = false;
                    if (!isPauseAnimationOnComplete)
                        Stop();
                }
                finishedAction?.Invoke();
            }
        }
    }
}