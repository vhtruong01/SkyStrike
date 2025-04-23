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
        Damage,
        Destruction
    }
    public class EnemyAnimationController : EnemyComponent
    {
        [SerializeField] private SpriteRenderer shieldSpriteRenderer;
        [SerializeField] private SpriteRenderer engineSpriteRenderer;
        [SerializeField] private SpriteRenderer highlightSpriteRenderer;
        private SpriteRenderer enemySpriteRenderer;
        private List<EnemyAnimation> animations;
        private SpriteChangeAnimation weaponAnimation;
        private SpriteChangeAnimation engineAnimation;
        private SpriteChangeAnimation destructionAnimation;
        private SpriteChangeAnimation shieldAnimation;
        private ValueChangeAnimation damageAnimation;

        public void Awake()
        {
            enemySpriteRenderer = GetComponent<SpriteRenderer>();
            EnableShieldImg(false);
            EnableEngineImg(false);
            shieldAnimation = new(shieldSpriteRenderer);
            shieldAnimation.SetStartedAction(() => EnableShieldImg(true))
                           .SetStoppedAction(() => EnableShieldImg(false));
            engineAnimation = new(engineSpriteRenderer, 0.1f);
            engineAnimation.SetLoop(true)
                           .SetStartedAction(() => EnableEngineImg(true))
                           .SetStoppedAction(() => EnableEngineImg(false));
            weaponAnimation = new(enemySpriteRenderer);
            weaponAnimation.SetLoop(true).SetStoppedAction(RestoreDefaultImg);
            destructionAnimation = new(enemySpriteRenderer);
            damageAnimation = new(HandleDamageTaken, 0, 1, 0.05f);
            damageAnimation.SetCancelWhenFinished(true)
                           .SetStartedAction(() => HandleDamageTaken(0))
                           .SetStoppedAction(() => HandleDamageTaken(0));
            animations = new()
            {
                shieldAnimation,
                engineAnimation,
                weaponAnimation,
                damageAnimation,
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
                case EAnimationType.Damage:
                    animation = damageAnimation;
                    break;
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
        public void Update()
        {
            foreach (EnemyAnimation animation in animations)
                animation.Animate(Time.deltaTime);
        }
        public override void Interrupt()
        {
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
        public void EnableHighlightImg(bool isEnabled)
            => highlightSpriteRenderer.gameObject.SetActive(isEnabled);
        private void RestoreDefaultImg()
            => enemySpriteRenderer.sprite = data.metaData.sprite;
        private void HandleDamageTaken(float alphaValue)
            => enemySpriteRenderer.material.SetFloat("_Alpha", alphaValue);
    }
}