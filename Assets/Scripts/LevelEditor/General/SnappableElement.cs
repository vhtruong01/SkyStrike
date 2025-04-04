using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class SnappableElement : DraggableElement
        {
            private readonly static float epsilon = 1f / 3;
            public static bool isSnap { get; private set; }

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
            public static bool IsSnap() => isSnap;
        }
    }
}