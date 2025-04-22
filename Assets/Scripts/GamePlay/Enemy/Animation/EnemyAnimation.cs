using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public enum EAnimationType
    {
        None = 0,
        Weapon,
        Engine,
        Shield,
        Destruction
    }
    public class EnemyAnimation
    {
        private readonly bool isLoop;
        private readonly float interval;
        private int curIndex;
        private float elapedTime;
        private bool isActive;
        private bool isAnimationFrozen;
        private List<Sprite> sprites;
        private SpriteRenderer spriteRenderer;
        private UnityAction finishedAction;
        public UnityAction startedAction { get; set; }
        public UnityAction stoppedAction { get; set; }

        public EnemyAnimation(SpriteRenderer spriteRenderer, float interval = 0.06f, bool isLoop = false)
        {
            this.spriteRenderer = spriteRenderer;
            this.interval = interval;
            this.isLoop = isLoop;
        }
        public void SetData(List<Sprite> sprites)
            => this.sprites = sprites;
        public void SetFinishedAction(UnityAction finishedAction)
            => this.finishedAction = finishedAction;
        public void Start()
        {
            if (isActive) return;
            isActive = true;
            isAnimationFrozen = false;
            Reset();
            startedAction?.Invoke();
        }
        private void Reset()
        {
            curIndex = 0;
            elapedTime = 0;
        }
        public void Stop()
        {
            isActive = false;
            stoppedAction?.Invoke();
        }
        public void Animate(float deltaTime)
        {
            if (!isActive || isAnimationFrozen || sprites.Count == 0) return;
            elapedTime += deltaTime;
            if (elapedTime < curIndex * interval) return;
            spriteRenderer.sprite = sprites[curIndex];
            curIndex++;
            if (curIndex == sprites.Count)
            {
                if (isLoop) Reset();
                else isAnimationFrozen = true;
                finishedAction?.Invoke();
            }
        }
    }
}