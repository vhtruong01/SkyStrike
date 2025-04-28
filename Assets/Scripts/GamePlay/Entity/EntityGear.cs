using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class EntityGear : MonoBehaviour, IEntityComponent
    {
        private readonly float maxSquaredLen = 0.5f;
        private readonly float moveTime = 1f;
        private bool isMoving = false;
        private Vector3 relativePos;
        public IEntity entity { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        private void Start()
            => relativePos = transform.position - entity.position;
        private void Update()
        {
            if (!isMoving && (relativePos + entity.position-transform.position).ToVector2().sqrMagnitude > maxSquaredLen)
                StartCoroutine(Move());
        }
        private IEnumerator Move()
        {
            isMoving = true;
            float elapsedTime = 0;
            float z = transform.position.z;
            Vector2 pos = transform.position;
            while (elapsedTime < moveTime)
            {
                transform.position = Vector2.Lerp(pos,
                    new(entity.position.x + relativePos.x, entity.position.y + relativePos.y),
                    elapsedTime / moveTime).SetZ(z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isMoving = false;
        }
        public void Interrupt() => gameObject.SetActive(false);
    }
}