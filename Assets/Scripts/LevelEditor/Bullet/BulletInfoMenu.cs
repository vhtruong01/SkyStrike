using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletInfoMenu : SubMenu<BulletDataObserver>
        {
            [SerializeField] private StringProperty bulletName;
            [SerializeField] private FloatProperty size;
            [SerializeField] private FloatProperty velocity;
            [SerializeField] private FloatProperty timeCooldown;
            [SerializeField] private FloatProperty spinSpeed;
            [SerializeField] private FloatProperty lifeTime;
            [SerializeField] private FloatProperty angleUnit;
            [SerializeField] private FloatProperty startAngle;
            [SerializeField] private Vector2Property spacing;
            [SerializeField] private Vector2Property position;
            [SerializeField] private BoolProperty isCircle;
            [SerializeField] private BoolProperty isStartAwake;
            [SerializeField] private BoolProperty isLookAtPlayer;
            [SerializeField] private IntProperty amount;

            public override void Init()
            {
                base.Init();
                isCircle.BindToOtherProperty(spacing, false);
                isCircle.BindToOtherProperty(angleUnit, false);
                isCircle.BindToOtherProperty(spinSpeed, true);
                isCircle.BindToOtherProperty(startAngle, false);
                isCircle.BindToOtherProperty(isLookAtPlayer, false);
                isLookAtPlayer.BindToOtherProperty(startAngle, false);
            }
            public override void BindData()
            {
                bulletName.Bind(data.name);
                size.Bind(data.size);
                velocity.Bind(data.velocity);
                timeCooldown.Bind(data.timeCooldown);
                spinSpeed.Bind(data.spinSpeed);
                lifeTime.Bind(data.lifeTime);
                angleUnit.Bind(data.angleUnit);
                startAngle.Bind(data.startAngle);
                spacing.Bind(data.spacing);
                position.Bind(data.position);
                isCircle.Bind(data.isCircle);
                isStartAwake.Bind(data.isStartAwake);
                isLookAtPlayer.Bind(data.isLookAtPlayer);
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
                lifeTime.Unbind();
                spacing.Unbind();
                position.Unbind();
                angleUnit.Unbind();
                isCircle.Unbind();
                isStartAwake.Unbind();
                isLookAtPlayer.Unbind();
                amount.Unbind();
            }
        }
    }
}