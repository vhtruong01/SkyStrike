using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveActionMenu : ActionMenu
        {
            [SerializeField] private FloatProperty dirX;
            [SerializeField] private FloatProperty dirY;
            [SerializeField] private FloatProperty rotation;
            [SerializeField] private FloatProperty delay;

            public override bool CanDisplay()
            {
                return true;
            }
            public override void Display(IData data)
            {

            }
            public override bool SetData(IData data)
            {
                return true;
            }
            public override void BindData()
            {
                throw new System.NotImplementedException();
            }
            public override void UnbindData()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}