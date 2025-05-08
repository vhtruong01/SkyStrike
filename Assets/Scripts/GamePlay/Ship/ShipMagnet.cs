using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class ShipMagnet : MonoBehaviour, IShipComponent
    {
        private Rigidbody2D rigi;
        private CircleCollider2D circleCollider;
        public IObject entity { get; set; }
        public ShipData shipData { get; set; }

        public void Init()
        {
            circleCollider = GetComponent<CircleCollider2D>();
            circleCollider.radius = shipData.magnetRadius;
            rigi = GetComponent<Rigidbody2D>();
            rigi.simulated = true;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IMagnetic>(out var obj) && obj.isMagnetic)
            {
                Vector2 s = entity.position - obj.position;
                Vector2 dir = Mathf.Sqrt(1 - Mathf.Clamp(s.magnitude / shipData.magnetRadius, 0, 0.99f)) * 0.1f * s;
                obj.HandleAffectedByGravity(dir);
            }
        }
        public void Interrupt() => rigi.simulated = false;
    }
}