using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class SnappableElement : DraggableElement
        {
            public static bool isSnap { get; private set; }
            private readonly static float epsilon = 1f / 3;

            protected override void SetPosition(Vector3 pos)
            {
                if (isSnap)
                {
                    pos.x = pos.x.Round(epsilon);
                    pos.y = pos.y.Round(epsilon);
                }
                base.SetPosition(pos);
            }
            public static void EnableSnapping(bool enable) => isSnap = enable;
        }
    }
}