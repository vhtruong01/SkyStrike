using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class EnemyAnimationController : EnemyComponent
    {
        [SerializeField] private SpriteRenderer shieldSpriteRenderer;
        [SerializeField] private SpriteRenderer engineSpriteRenderer;
        private SpriteRenderer enemySpriteRenderer;
        private List<EnemyAnimation> animations;
        private EnemyAnimation weaponAnimation;
        private EnemyAnimation engineAnimation;
        private EnemyAnimation destructionAnimation;
        private EnemyAnimation shieldAnimation;

        public void Awake()
        {
            enemySpriteRenderer = GetComponent<SpriteRenderer>();
            EnableShieldImg(false);
            EnableEngineImg(false);
            shieldAnimation = new(shieldSpriteRenderer)
            {
                startedAction = () => EnableShieldImg(true),
                stoppedAction = () => EnableShieldImg(false)
            };
            engineAnimation = new(engineSpriteRenderer, interval: 0.1f, true)
            {
                startedAction = () => EnableEngineImg(true),
                stoppedAction = () => EnableEngineImg(false)
            };
            weaponAnimation = new(enemySpriteRenderer, isLoop: true)
            {
                stoppedAction = RestoreDefaultImg
            };
            destructionAnimation = new(enemySpriteRenderer);
            animations = new()
            {
                shieldAnimation,
                engineAnimation,
                weaponAnimation,
                destructionAnimation,
            };
        }
        public override void SetData(EnemyData data)
        {
            base.SetData(data);
            shieldAnimation.SetData(data.metaData.shieldSprites);
            engineAnimation.SetData(data.metaData.engineSprites);
            weaponAnimation.SetData(data.metaData.weaponSprites);
            destructionAnimation.SetData(data.metaData.destructionSprites);
        }
        public void SetTrigger(EAnimationType type, bool state = true, UnityAction finishedAction = null)
        {
            EnemyAnimation animation = null;
            switch (type)
            {
                case EAnimationType.Destruction:
                    animation = destructionAnimation;
                    break;
                case EAnimationType.Engine:
                    animation = engineAnimation;
                    break;
                case EAnimationType.Weapon:
                    animation = weaponAnimation;
                    break;
                case EAnimationType.Shield:
                    animation = shieldAnimation;
                    break;
            }
            ActiveAnimation(animation, state, finishedAction);
        }
        public void FixedUpdate()
        {
            foreach (EnemyAnimation animation in animations)
                animation.Animate(Time.fixedDeltaTime);
        }
        public override void Interrupt()
        {
            EnableShieldImg(false);
            foreach (EnemyAnimation animation in animations)
                animation.Stop();
        }
        private void ActiveAnimation(EnemyAnimation animation, bool state, UnityAction finishedAction)
        {
            if (animation == null) return;
            if (state)
            {
                animation.SetFinishedAction(finishedAction);
                animation.Start();
            }
            else animation.Stop();
        }
        private void EnableShieldImg(bool isEnabled)
        {
            if (isEnabled && data.metaData.shieldSprites.Count > 0)
                shieldSpriteRenderer.sprite = data.metaData.shieldSprites[0];
            shieldSpriteRenderer.gameObject.SetActive(isEnabled);
        }
        private void EnableEngineImg(bool isEnabled)
        {
            if (isEnabled && data.metaData.engineSprites.Count > 0)
                engineSpriteRenderer.sprite = data.metaData.engineSprites[0];
            engineSpriteRenderer.gameObject.SetActive(isEnabled);
        }
        private void RestoreDefaultImg()
            => enemySpriteRenderer.sprite = data.metaData.sprite;
    }
}