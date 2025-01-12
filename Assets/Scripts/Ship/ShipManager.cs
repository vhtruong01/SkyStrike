using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyStrike
{
    namespace Ship
    {
        [RequireComponent(typeof(Entity))]
        [RequireComponent(typeof(PlayerInput))]
        [RequireComponent(typeof(Attack))]
        [RequireComponent(typeof(Movement))]
        [RequireComponent(typeof(BulletManager))]

        public class ShipManager : MonoBehaviour
        {
        }
    }
}
