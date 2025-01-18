using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public struct MoveData
        {
            public Vector2 distance;
            public float rotation;
            public float speed;
            public float delay;
            public bool isSyncRot;
        }
    }
}