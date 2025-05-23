using UnityEngine;

namespace SkyStrike.Game
{
    public sealed class ShipGear : MonoBehaviour, IShipComponent
    {
        [SerializeField, Range(0f, 1f)] private float velocityCoefficient;
        private Vector3 relativePos;
        public IObject entity { get; set; }
        public ShipData shipData { get; set; }

        public void Init()
            => relativePos = transform.position - entity.position;
        private void OnEnable()
        {
            if (entity != null)
                transform.position = relativePos + entity.position;
        }
        private void Update()
        {
            Vector2 target = entity.position + relativePos;
            if (transform.position.x != target.x || transform.position.y != target.y)
                transform.position = Vector2.MoveTowards(transform.position, target, Time.unscaledDeltaTime * shipData.speed * velocityCoefficient).SetZ(transform.position.z);
        }
        public void Interrupt() { }
    }
}