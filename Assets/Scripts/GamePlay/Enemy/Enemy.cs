using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyBulletSpawner))]
    public class Enemy : PoolableObject<ObjectData>
    {
        private EnemyMovement movement;
        private EnemyBulletSpawner spawner;
        private float spawnInterval;
        private int hp;
        private bool isDie;
        public EItem dropItemType { get; set; }

        public override void Awake()
        {
            base.Awake();
            movement = GetComponent<EnemyMovement>();
            spawner = GetComponent<EnemyBulletSpawner>();
        }
        public override void SetData(ObjectData data)
        {
            base.SetData(data);
            hp = data.metaData.maxHp;
            spriteRenderer.sprite = data.metaData.sprite;
            col.size = data.metaData.sprite.bounds.size / 2;
            spriteRenderer.color = data.metaData.color;
            transform.localScale *= data.size;
            spawnInterval = data.spawnInterval;
            isDie = false;
        }
        public void Strike(float waveDelay, int queueIndex)
            => StartCoroutine(Prepare(waveDelay, queueIndex));
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
            spawner.isSpawn = false;
            movement.StopAllCoroutines();
            EventManager.DropStar(transform.position, data.metaData.star);
            if (dropItemType != EItem.None)
                EventManager.DropItem(data.dropItemType, transform.position);
            Disappear();
        }
        private IEnumerator Prepare(float waveDelay, int queueIndex)
        {
            Enable(false);
            yield return new WaitForSeconds(waveDelay + data.moveData.delay + spawnInterval * queueIndex);
            Enable(true);
            StartCoroutine(movement.Move());
        }
        private void Enable(bool isEnabled)
        {
            col.enabled = isEnabled;
            spriteRenderer.enabled = isEnabled;
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ShipBullet") && collision.TryGetComponent<ShipBullet>(out var bullet))
            {
                if (TakeDamage(bullet.data.dmg))
                    bullet.Disappear();
            }
        }
    }
}