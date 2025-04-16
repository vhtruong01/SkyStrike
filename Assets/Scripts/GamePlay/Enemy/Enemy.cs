using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [RequireComponent(typeof(EnemyMovement))]
        public class Enemy : PoolableObject<ObjectData>
        {
            private ObjectData data;
            private EnemyMovement movement;
            private float spawnInterval;
            private int hp;
            private bool isDie;
            public EItem dropItemType { get; set; }

            public override void Awake()
            {
                base.Awake();
                movement = GetComponent<EnemyMovement>();
            }
            public override void SetData(ObjectData data)
            {
                this.data = data;
                hp = data.metaData.maxHp;
                spriteRenderer.sprite = data.metaData.sprite;
                col.size = data.metaData.sprite.bounds.size / 2;
                spriteRenderer.color = data.metaData.color;
                transform.localScale *= data.size;
                spawnInterval = data.spawnInterval;
                isDie = false;
            }
            public void Strike(MoveData moveData, float waveDelay, int queueIndex)
                => StartCoroutine(Prepare(moveData, waveDelay, queueIndex));
            public bool TakeDamage(int dmg)
            {
                if (!isDie)
                {
                    hp -= dmg;
                    if (hp <= 0)
                        Die();
                    return true;
                }
                return false;
            }
            public void Die()
            {
                isDie = true;
                EventManager.DropStar(transform.position, data.metaData.star);
                if (dropItemType != EItem.None)
                    EventManager.DropItem(data.dropItemType, transform.position);
                Release();
            }
            private IEnumerator Prepare(MoveData moveData, float waveDelay, int queueIndex)
            {
                Enable(false);
                yield return new WaitForSeconds(waveDelay + moveData.delay + spawnInterval * queueIndex);
                Enable(true);
                StartCoroutine(movement.Move(moveData));
            }
            private void Enable(bool isEnabled)
            {
                col.enabled = isEnabled;
                spriteRenderer.enabled = isEnabled;
            }
        }
    }
}