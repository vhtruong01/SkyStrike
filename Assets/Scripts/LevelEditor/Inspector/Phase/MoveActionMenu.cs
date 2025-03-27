using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveActionMenu : ActionMenu
        {
            [SerializeField] protected BoolProperty syncRotation;
            //[SerializeField] protected FloatProperty dirX;
            //[SerializeField] protected FloatProperty dirY;
            //[SerializeField] protected FloatProperty radius;
            [SerializeField] protected FloatProperty rotation;
            [SerializeField] protected FloatProperty scale;
            [SerializeField] protected FloatProperty accleration;

            public override void BindData()
            {
                base.BindData();
                if (data is not MoveDataObserver moveActionData) return;
                //dirX.Bind(moveActionData.dirX);
                //dirY.Bind(moveActionData.dirY);
                //radius.Bind(moveActionData.radius);
                rotation.Bind(moveActionData.rotation);
                scale.Bind(moveActionData.scale);
                accleration.Bind(moveActionData.accleration);
                syncRotation.Bind(moveActionData.isSyncRotation);
            }
            public override void UnbindData()
            {
                base.UnbindData();
                if (data is not MoveDataObserver moveActionData) return;
                //dirX.Unbind();
                //dirY.Unbind();
                //radius.Unbind();
                rotation.Unbind();
                delay.Unbind();
                scale.Unbind();
                accleration.Unbind();
                syncRotation.Unbind();
            }
        }
    }
}