using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class EnemyAnimator : MonoBehaviour, IEnemyComponent, IAnimator
    {
        [SerializeField] private SpriteRenderer shieldSpriteRenderer;
        [SerializeField] private SpriteRenderer engineSpriteRenderer;
        [SerializeField] private SpriteRenderer highlightSpriteRenderer;
        private SpriteRenderer enemySpriteRenderer;
        private List<IEntityAnimation> animations;
        private SpriteChangeAnimation weaponAnimation;
        private SpriteChangeAnimation engineAnimation;
        private SpriteChangeAnimation destructionAnimation;
        private SpriteChangeAnimation shieldAnimation;
        private ValueChangeAnimation damageAnimation;
        private ValueChangeAnimation highlightAnimation;
        public EnemyData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Awake()
        {
            data = GetComponent<EnemyData>();
            enemySpriteRenderer = GetComponent<SpriteRenderer>();
            EnableShieldImg(false);
            EnableEngineImg(false);
            shieldAnimation = new(shieldSpriteRenderer, 0.06f);
            shieldAnimation.SetStartedAction(() => EnableShieldImg(true))
                           .SetStoppedAction(() => EnableShieldImg(false))
                           .PauseAnimationOnComplete(true);
            engineAnimation = new(engineSpriteRenderer, 0.1f);
            engineAnimation.SetStartedAction(() => EnableEngineImg(true))
                           .SetStoppedAction(() => EnableEngineImg(false))
                           .SetLoop(true);
            weaponAnimation = new(enemySpriteRenderer, 0.075f);
            weaponAnimation.SetStoppedAction(RestoreDefaultImg)
                           .SetLoop(true);
            damageAnimation = new(ChangeEnemyMaterialAlpha, 0, 1, 0.025f);
            damageAnimation.SetStartedAction(() => ChangeEnemyMaterialAlpha(0))
                           .SetStoppedAction(() => ChangeEnemyMaterialAlpha(0));
            highlightAnimation = new(ChangeHighlightMaterialAlpha, 0, 1, 1);
            highlightAnimation.SetStartedAction(() => EnableHighlightImg(true))
                              .SetStoppedAction(() => EnableHighlightImg(false))
                              .SetDirection(EAnimationDirection.LRL)
                              .SetLoop(true);
            destructionAnimation = new(enemySpriteRenderer, 0.05f);
            animations = new()
            {
                shieldAnimation,
                engineAnimation,
                weaponAnimation,
                damageAnimation,
                destructionAnimation,
            };
        }
        public IEntityAnimation SetTrigger(EAnimationType type)
        {
            IEntityAnimation animation = null;
            switch (type)
            {
                case EAnimationType.Damage:
                    animation = damageAnimation;
                    break;
                case EAnimationType.Destruction:
                    destructionAnimation.SetData(data.metaData.destructionSprites);
                    animation = destructionAnimation;
                    break;
                case EAnimationType.Engine:
                    engineAnimation.SetData(data.metaData.engineSprites);
                    animation = engineAnimation;
                    break;
                case EAnimationType.Weapon:
                    weaponAnimation.SetData(data.metaData.weaponSprites);
                    animation = weaponAnimation;
                    break;
                case EAnimationType.Shield:
                    shieldAnimation.SetData(data.metaData.shieldSprites);
                    animation = shieldAnimation;
                    break;
                case EAnimationType.HighLight:
                    animation = highlightAnimation;
                    break;
            }
            return animation;
        }
        public void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (var animation in animations)
                animation.Animate(deltaTime);
        }
        public void Interrupt()
        {
            foreach (var animation in animations)
                animation.Stop();
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
        private void EnableHighlightImg(bool isEnabled)
            => highlightSpriteRenderer.gameObject.SetActive(isEnabled);
        private void ChangeEnemyMaterialAlpha(float alphaValue)
            => enemySpriteRenderer.material.SetFloat("_Alpha", alphaValue);
        private void ChangeHighlightMaterialAlpha(float alphaValue)
            => highlightSpriteRenderer.material.SetFloat("_Alpha", alphaValue);
    }
}