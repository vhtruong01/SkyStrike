using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionMenu : SubMenu<ActionDataObserver>
        {
            [SerializeField] private BoolProperty isLoop;
            [SerializeField] protected FloatProperty delay;

            public override void BindData()
            {
                isLoop.Bind(data.isLoop);
                delay.Bind(data.delay);
            }
            public override void UnbindData()
            {
                isLoop.Unbind();
                delay.Unbind();
            }
        }
    }
}