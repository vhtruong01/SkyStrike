using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ShipMagnet : MonoBehaviour, IEntityComponent
    {
        private float radius;
        private Rigidbody2D rigi;
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Awake()
        {
            radius = GetComponent<CircleCollider2D>().radius;
            rigi = GetComponent<Rigidbody2D>();
            rigi.simulated = true;
        }
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Item") && collision.TryGetComponent<IMagnetic>(out var obj) && obj.isMagnetic)
            {
                Vector2 dir = transform.position - obj.gameObject.transform.position;
                obj.HandleAffectedByGravity(Mathf.Sqrt(1 - Mathf.Clamp(dir.magnitude / radius, 0, 0.99f)) * 0.1f * dir);
            }
        }
        public void Interrupt() => rigi.simulated = false;
    }
}