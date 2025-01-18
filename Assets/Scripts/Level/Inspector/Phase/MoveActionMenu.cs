using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class MoveActionMenu : ActionMenu
        {
            [SerializeField] private TMP_InputField x;
            [SerializeField] private TMP_InputField y;
            [SerializeField] private TMP_InputField rotation;
            [SerializeField] private TMP_InputField delay;

            public override void Clear()
            {
            }

            public override void Display(ActionUI actionUI)
            {
            }
        }
    }
}