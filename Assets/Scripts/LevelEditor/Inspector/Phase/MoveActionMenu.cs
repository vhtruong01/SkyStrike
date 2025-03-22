using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveActionMenu : ActionMenu
        {
            [SerializeField] protected BoolProperty syncRotation;
            [SerializeField] protected FloatProperty dirX;
            [SerializeField] protected FloatProperty dirY;
            [SerializeField] protected FloatProperty rotation;
            [SerializeField] protected FloatProperty scale;
            [SerializeField] protected FloatProperty accleration;
            [SerializeField] protected FloatProperty radius;

            public override void BindData()
            {
                base.BindData();
                if (data is not MoveDataObserver moveActionData) return;
                dirX.Bind(moveActionData.dirX);
                dirY.Bind(moveActionData.dirY);
                rotation.Bind(moveActionData.rotation);
                scale.Bind(moveActionData.scale);
                accleration.Bind(moveActionData.accleration);
                radius.Bind(moveActionData.radius);
                syncRotation.Bind(moveActionData.isSyncRotation);
            }
            public override void UnbindData()
            {
                base.UnbindData();
                if (data is not MoveDataObserver moveActionData) return;
                dirX.Unbind();
                dirY.Unbind();
                rotation.Unbind();
                delay.Unbind();
                scale.Unbind();
                radius.Unbind();
                accleration.Unbind();
                syncRotation.Unbind();
            }
        }
    }
}