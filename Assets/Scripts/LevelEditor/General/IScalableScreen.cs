using UnityEngine;

namespace SkyStrike.Editor
{
    public interface IScalableScreen : IInitalizable
    {
        public Camera cam { get; }
        public float scale { get; set; }
        public bool isSnapping { get; set; }
        public Vector2 GetActualPosition(Vector2 pos);
        public Vector2 GetPositionOnScreen(Vector2 pos);
        public Vector2 RoundPosition(Vector2 pos);
    }
}