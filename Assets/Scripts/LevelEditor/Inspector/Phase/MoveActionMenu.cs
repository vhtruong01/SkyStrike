using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveActionMenu : ActionMenu
        {
            [SerializeField] private BoolProperty syncRotation;
            [SerializeField] private FloatProperty dirX;
            [SerializeField] private FloatProperty dirY;
            [SerializeField] private FloatProperty rotation;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private FloatProperty scale;
            
            public override void BindData()
            {
                var moveActionData= actionData as EnemyMoveDataObserver;
                if (moveActionData == null) return;
                dirX.Bind(moveActionData.dirX);
                dirX.Bind(moveActionData.dirY);
                rotation.Bind(moveActionData.rotation);
                delay.Bind(moveActionData.delay);
                scale.Bind(moveActionData.scale);
                syncRotation.Bind(moveActionData.isSyncRotation);
            }
            public override void UnbindData()
            {
                var moveActionData = actionData as EnemyMoveDataObserver;
                if (moveActionData == null) return;
                dirX.Unbind();
                dirY.Unbind();
                rotation.Unbind();
                delay.Unbind();
                scale.Unbind();
                syncRotation.Unbind();
            }
        }
    }
}