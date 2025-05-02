using UnityEngine;

namespace SkyStrike.Editor
{
    public class BulletInfoMenu : SubMenu<BulletDataObserver>
    {
        [SerializeField] private StringProperty bulletName;
        [SerializeField] private FloatProperty size;
        [SerializeField] private FloatProperty velocity;
        [SerializeField] private FloatProperty timeCooldown;
        [SerializeField] private FloatProperty spinSpeed;
        [SerializeField] private FloatProperty lifetime;
        [SerializeField] private FloatProperty unitAngle;
        [SerializeField] private FloatProperty startAngle;
        [SerializeField] private Vector2Property spacing;
        [SerializeField] private Vector2Property position;
        [SerializeField] private BoolProperty isCircle;
        [SerializeField] private BoolProperty isStartAwake;
        [SerializeField] private BoolProperty isLookingAtPlayer;
        [SerializeField] private IntProperty amount;

        public override void Init()
        {
            base.Init();
            isCircle.BindToOtherProperty(spacing, false);
            isCircle.BindToOtherProperty(unitAngle, false);
            isCircle.BindToOtherProperty(spinSpeed, true);
            isCircle.BindToOtherProperty(startAngle, false);
            isCircle.BindToOtherProperty(isLookingAtPlayer, false);
            isLookingAtPlayer.BindToOtherProperty(startAngle, false);
        }
        public override void BindData()
        {
            bulletName.Bind(data.name);
            size.Bind(data.size);
            velocity.Bind(data.velocity);
            timeCooldown.Bind(data.timeCooldown);
            spinSpeed.Bind(data.spinSpeed);
            lifetime.Bind(data.lifetime);
            unitAngle.Bind(data.unitAngle);
            startAngle.Bind(data.startAngle);
            spacing.Bind(data.spacing);
            position.Bind(data.position);
            isCircle.Bind(data.isCircle);
            isStartAwake.Bind(data.isStartAwake);
            isLookingAtPlayer.Bind(data.isLookingAtPlayer);
            amount.Bind(data.amount);
        }
        public override void UnbindData()
        {
            bulletName.Unbind();
            size.Unbind();
            velocity.Unbind();
            timeCooldown.Unbind();
            spinSpeed.Unbind();
            startAngle.Unbind();
            lifetime.Unbind();
            spacing.Unbind();
            position.Unbind();
            unitAngle.Unbind();
            isCircle.Unbind();
            isStartAwake.Unbind();
            isLookingAtPlayer.Unbind();
            amount.Unbind();
        }
    }
}