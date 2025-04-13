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
            [SerializeField] private FloatProperty length;
            [SerializeField] private FloatProperty angleUnit;
            [SerializeField] private Vector2Property spacing;
            [SerializeField] private Vector2Property position;
            [SerializeField] private BoolProperty isCircle;
            [SerializeField] private BoolProperty isStartAwake;
            [SerializeField] private IntProperty amount;

            public void Awake()
            {
                isCircle.BindToOtherProperty(spacing, false);
                isCircle.BindToOtherProperty(angleUnit, false);
                isCircle.BindToOtherProperty(length, false);
                isCircle.BindToOtherProperty(spinSpeed, true);
                Init();
            }
            public override void BindData()
            {
                bulletName.Bind(data.name);
                size.Bind(data.size);
                velocity.Bind(data.velocity);
                timeCooldown.Bind(data.timeCooldown);
                spinSpeed.Bind(data.spinSpeed);
                length.Bind(data.length);
                angleUnit.Bind(data.angleUnit);
                spacing.Bind(data.spacing);
                position.Bind(data.position);
                isCircle.Bind(data.isCircle);
                isStartAwake.Bind(data.isStartAwake);
                amount.Bind(data.amount);
            }
            public override void UnbindData()
            {
                bulletName.Unbind();
                size.Unbind();
                velocity.Unbind();
                timeCooldown.Unbind();
                spinSpeed.Unbind();
                length.Unbind();
                spacing.Unbind();
                position.Unbind();
                angleUnit.Unbind();
                isCircle.Unbind();
                isStartAwake.Unbind();
                amount.Unbind();
            }
        }
    }
}