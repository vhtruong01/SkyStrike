using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class SpriteChangeAnimation : EnemyAnimation
    {
        protected int curIndex;
        protected float interval = 0.06f;
        protected SpriteRenderer spriteRenderer;
        protected List<Sprite> sprites;

        public SpriteChangeAnimation(SpriteRenderer spriteRenderer, float interval = 0.06f)
        {
            this.interval = interval;
            this.spriteRenderer = spriteRenderer;
        }
        public void SetData(List<Sprite> sprites)
            => this.sprites = sprites;
        protected override void Reset()
        {
            elapedTime = 0;
            curIndex = 0;
        }
        public override void Animate(float deltaTime)
        {
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
                    if (isCancelWhenFinish)
                        Stop();
                }
                finishedAction?.Invoke();
            }
        }
    }
}