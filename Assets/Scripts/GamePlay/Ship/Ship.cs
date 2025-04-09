using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [RequireComponent(typeof(ShipBulletManager))]
        public class Ship : MonoBehaviour
        {
            private ShipBulletManager bulletManager;

            public void Awake()
            {
                bulletManager = GetComponent<ShipBulletManager>();

            }
        }
    }
}