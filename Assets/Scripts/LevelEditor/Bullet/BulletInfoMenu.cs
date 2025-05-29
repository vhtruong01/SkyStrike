using UnityEngine;

namespace SkyStrike.Editor
{
    public class BulletInfoMenu : SubMenu<BulletDataObserver>
    {
        [SerializeField] private StringProperty bulletName;
        [SerializeField] private FloatProperty size;
        [SerializeField] private FloatProperty speed;
        [SerializeField] private FloatProperty timeCooldown;
        [SerializeField] private FloatProperty spinSpeed;
        [SerializeField] private FloatProperty lifetime;
        [SerializeField] private FloatProperty unitAngle;
        [SerializeField] private FloatProperty startAngle;
        [SerializeField] private FloatProperty delay;
        [SerializeField] private Vector2Property spacing;
        [SerializeField] private Vector2Property position;
        [SerializeField] private BoolProperty isCircle;
        [SerializeField] private BoolProperty isStartAwake;
        [SerializeField] private BoolProperty isUseState;
        [SerializeField] private IntProperty amount;
        [SerializeField] private IntProperty stack;

        protected override void Preprocess()
        {
            base.Preprocess();
            isCircle.BindToOtherProperty(spacing, false);
            isCircle.BindToOtherProperty(unitAngle, false);
            isCircle.BindToOtherProperty(spinSpeed, true);
            isCircle.BindToOtherProperty(startAngle, false);
            isUseState.BindToOtherProperty(lifetime, false);
            isUseState.BindToOtherProperty(size, false);
        }
        public override void BindData()
        {
            bulletName.Bind(data.name);
            size.Bind(data.size);
            speed.Bind(data.speed);
            timeCooldown.Bind(data.timeCooldown);
            spinSpeed.Bind(data.spinSpeed);
            lifetime.Bind(data.lifetime);
            unitAngle.Bind(data.unitAngle);
            startAngle.Bind(data.startAngle);
            delay.Bind(data.delay);
            spacing.Bind(data.spacing);
            position.Bind(data.position);
            isCircle.Bind(data.isCircle);
            isStartAwake.Bind(data.isStartAwake);
            isUseState.Bind(data.isUseState);
            amount.Bind(data.amount);
            stack.Bind(data.stack);
        }
        public override void UnbindData()
        {
            bulletName.Unbind();
            size.Unbind();
            speed.Unbind();
            timeCooldown.Unbind();
            spinSpeed.Unbind();
            startAngle.Unbind();
            lifetime.Unbind();
            delay.Unbind();
            spacing.Unbind();
            position.Unbind();
            unitAngle.Unbind();
            isCircle.Unbind();
            isStartAwake.Unbind();
            isUseState.Unbind();
            amount.Unbind();
            stack.Unbind();
        }
    }
}