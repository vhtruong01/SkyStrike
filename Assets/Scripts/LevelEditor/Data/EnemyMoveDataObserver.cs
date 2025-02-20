using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyMoveDataObserver : IEnemyActionDataObserver
        {
            private DataObserver<Vector2> distance;

            private DataObserver<float> rotation;
            private DataObserver<bool> isSyncRotation;
        }
    }
}