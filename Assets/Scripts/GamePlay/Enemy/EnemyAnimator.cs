using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyAnimator : EntityAnimator, IEnemyComponent, IAnimator
    {
        [SerializeField] private SpriteAnimation shieldAnimation;
        [SerializeField] private SpriteAnimation engineAnimation;
        [SerializeField] private SpriteAnimation mainWeaponAnimation;
        [SerializeField] private SpriteAnimation destructionAnimation;
        [SerializeField] private MaterialAlphaAnimation damagedAnimation;
        public EnemyData data { get; set; }

        public override IAnimation SetTrigger(EAnimationType type)
        {
            switch (type)
            {
                case EAnimationType.Destruction:
                    destructionAnimation.SetData(data.metaData.destructionSprites);
                    break;
                case EAnimationType.Engine:
                    engineAnimation.SetData(data.metaData.engineSprites);
                    break;
                case EAnimationType.MainWeapon:
                    mainWeaponAnimation.SetData(data.metaData.weaponSprites);
                    break;
                case EAnimationType.Shield:
                    shieldAnimation.SetData(data.metaData.shieldSprites);
                    break;
                case EAnimationType.Damaged:
                    damagedAnimation.SetData(data.metaData.sprite);
                    break;
            }
            return base.SetTrigger(type);
        }
    }
}