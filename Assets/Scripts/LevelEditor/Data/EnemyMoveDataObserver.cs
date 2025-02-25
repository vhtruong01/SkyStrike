using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyMoveDataObserver : IEnemyActionDataObserver
        {
            private DataObserver<float> dirX;
            private DataObserver<float> dirY;
            private DataObserver<float> rotation;
            private DataObserver<bool> isSyncRotation;
        }
    }
}