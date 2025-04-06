using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Enemy : MonoBehaviour, IGameObject<ObjectData>
        {
            private SpriteRenderer spriteRenderer;
            private EnemyMovement movement;
            private Transform body;
            private BoxCollider2D col;
            private float spawnInterval;

            public void Awake()
            {
                body = transform.GetChild(0);
                col = body.GetComponent<BoxCollider2D>();
                spriteRenderer = body.GetComponent<SpriteRenderer>();
                movement = GetComponent<EnemyMovement>();
            }
            public void SetData(ObjectData data)
            {
                spriteRenderer.sprite = data.metaData.sprite;
                col.size = data.metaData.sprite.bounds.size / 2;
                spriteRenderer.color = data.metaData.color;
                transform.localScale *= data.size;
                spawnInterval = data.spawnInterval;
            }
            public void Strike(MoveData moveData, int queueIndex = 0)
                => StartCoroutine(Prepare(moveData, queueIndex));
            private IEnumerator Prepare(MoveData moveData, int queueIndex = 0)
            {
                body.gameObject.SetActive(false);
                yield return new WaitForSeconds(moveData.delay + spawnInterval * queueIndex);
                body.gameObject.SetActive(true);
                StartCoroutine(movement.Move(moveData));
            }
        }
    }
}