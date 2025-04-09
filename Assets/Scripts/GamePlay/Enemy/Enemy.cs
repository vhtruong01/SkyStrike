using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [RequireComponent(typeof(EnemyMovement))]
        public class Enemy : PoolableObject
        {
            private EnemyMovement movement;
            private Transform body;
            private float spawnInterval;
            private int hp;

            public override void Awake()
            {
                base.Awake();
                body = transform.GetChild(0);
                movement = GetComponent<EnemyMovement>();
            }
            public void SetData(ObjectData data)
            {
                hp = 1000;
                spriteRenderer.sprite = data.metaData.sprite;
                col.size = data.metaData.sprite.bounds.size / 2;
                spriteRenderer.color = data.metaData.color;
                transform.localScale *= data.size;
                spawnInterval = data.spawnInterval;
            }
            public void Strike(MoveData moveData, float waveDelay, int queueIndex = 0)
                => StartCoroutine(Prepare(moveData, waveDelay, queueIndex));

            public void TakeDamage(int dmg)
            {
                hp -= dmg;
                if (hp <= 0)
                    Die();
            }
            public void Die()
            {
                print("die");
                //EventManager.DropItem(EItem.Star1,transform.position);
                EventManager.DropStar( transform.position,5);
                Release();
            }
            private IEnumerator Prepare(MoveData moveData, float waveDelay, int queueIndex)
            {
                body.gameObject.SetActive(false);
                yield return new WaitForSeconds(waveDelay + moveData.delay + spawnInterval * queueIndex);
                body.gameObject.SetActive(true);
                StartCoroutine(movement.Move(moveData));
            }
        }
    }
}