using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ShipMagnet : MonoBehaviour, IShipComponent
    {
        private Rigidbody2D rigi;
        public IEntity entity { get; set; }
        public ShipData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Init()
        {
            GetComponent<CircleCollider2D>().radius = data.metaData.magnetRadius;
            rigi = GetComponent<Rigidbody2D>();
            rigi.simulated = true;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IMagnetic>(out var obj) && obj.isMagnetic)
            {
                Vector2 dir = entity.position - obj.gameObject.transform.position;
                obj.HandleAffectedByGravity(Mathf.Sqrt(1 - Mathf.Clamp(dir.magnitude / data.metaData.magnetRadius, 0, 0.99f)) * 0.1f * dir);
            }
        }
        public void Interrupt() => rigi.simulated = false;
    }
}