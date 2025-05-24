using UnityEngine;

namespace SkyStrike.Editor
{
    public class BulletStateInfoMenu : SubMenu<BulletStateDataObserver>
    {
        [SerializeField] private FloatProperty coef;
        [SerializeField] private FloatProperty scale;
        [SerializeField] private FloatProperty rotation;
        [SerializeField] private FloatProperty duration;
        [SerializeField] private FloatProperty transtionDuration;
        [SerializeField] private BoolProperty isAuto;

        protected override void Preprocess()
        {
            base.Preprocess();
            isAuto.BindToOtherProperty(rotation, false);
        }
        public override void BindData()
        {
            coef.Bind(data.coef);
            scale.Bind(data.scale);
            rotation.Bind(data.rotation);
            duration.Bind(data.duration);
            isAuto.Bind(data.isAuto);
            transtionDuration.Bind(data.transitionDuration);
        }
        public override void UnbindData()
        {
            coef.Unbind();
            scale.Unbind();
            rotation.Unbind();
            duration.Unbind();
            isAuto.Unbind();
            transtionDuration.Unbind();
        }
    }
}