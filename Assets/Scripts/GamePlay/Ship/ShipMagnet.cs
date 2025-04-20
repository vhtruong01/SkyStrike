using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ShipMagnet : MonoBehaviour
    {
        private float r;

        public void Awake()
        {
            r = GetComponent<CircleCollider2D>().radius;
        }
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IPullable>(out var obj))
            {
                Vector2 dir = transform.position - obj.gameObject.transform.position;
                obj.HandleAffectedByGravity(Mathf.Sqrt(1 - Mathf.Clamp(dir.magnitude / r, 0, 0.99f)) * 0.2f * dir);
            }
        }
    }
}