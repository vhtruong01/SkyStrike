using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IScalableScreen
        {
            public float scale { get; set; }
            public bool isSnapping { get; set; }
            public Vector2 GetActualPosition(Vector2 pos);
            public Vector2 GetPositionOnScreen(Vector2 pos);
            public Vector2 RoundPosition(Vector2 pos);
        }
    }
}