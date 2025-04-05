using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IScalableScreen
        {
            public Vector2 GetActualPosition(Vector2 pos);
            public Vector2 GetPositionOnScreen(Vector2 pos);
            public Vector2 RoundPosition(Vector2 pos);
            public bool IsSnap();
            public void EnableSnap(bool snap);
            public void Init();
        }
    }
}