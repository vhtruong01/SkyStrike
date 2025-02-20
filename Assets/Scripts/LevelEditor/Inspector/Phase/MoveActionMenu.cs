using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveActionMenu : ActionMenu
        {
            [SerializeField] private TMP_InputField posX;
            [SerializeField] private TMP_InputField posY;
            [SerializeField] private TMP_InputField rotation;
            [SerializeField] private TMP_InputField delay;

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
        }
    }
}