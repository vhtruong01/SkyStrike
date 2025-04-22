using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum EEnemyAction
    {
        None = 0,
        Stand,
        Attack,
        Defend,
        Move,
        Die,
        Arrive,
        Disappear
    }
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyBulletSpawner))]
    [RequireComponent(typeof(EnemyAnimationController))]
    [RequireComponent(typeof(EnemyCommand))]
    public class Enemy : PoolableObject<EnemyData>
    {
        private EnemyComponent[] enemyComponents;
        private EnemyBulletSpawner spawner;
        private EnemyMovement movement;
        private EnemyAnimationController animController;

        public override void Awake()
        {
            base.Awake();
            movement = GetComponent<EnemyMovement>();
            spawner = GetComponent<EnemyBulletSpawner>();
            enemyComponents = GetComponents<EnemyComponent>();
            animController = GetComponent<EnemyAnimationController>();
        }
        public override void SetData(EnemyData data)
        {
            base.SetData(data);
            data.hp = data.metaData.maxHp;
            spriteRenderer.sprite = data.metaData.sprite;
            col.size = data.metaData.sprite.bounds.size / 2;
            spriteRenderer.color = data.metaData.color;
            transform.localScale = Vector3.one * data.size;
            data.isDie = false;
            foreach (var comp in enemyComponents)
            {
                comp.SetData(data);
                comp.notifyAction = HandleEvent;
            }
        }
        private void HandleEvent(EEnemyAction action)
        {
            switch (action)
            {
                case EEnemyAction.Stand:
                    animController.SetTrigger(EAnimationType.Engine, false);
                    break;
                case EEnemyAction.Move:
                    animController.SetTrigger(EAnimationType.Engine, true);
                    break;
                case EEnemyAction.Attack:
                    spawner.Spawn();
                    break;
                case EEnemyAction.Defend:
                    animController.SetTrigger(EAnimationType.Shield, data.shield);
                    break;
                case EEnemyAction.Die:
                    animController.SetTrigger(EAnimationType.Destruction, true, DropItemAndDisappear);
                    break;
                case EEnemyAction.Disappear:
                    if (!data.isMaintain)
                        Disappear();
                    break;
            }
        }
        public void Strike(float delay)
            => StartCoroutine(Prepare(delay));
        public bool TakeDamage(int dmg)
        {
            if (data.isDie || data.isImmortal) return false;
            if (!data.shield)
            {
                data.hp -= dmg;
                if (data.hp <= 0)
                    Die();
            }
            return true;
        }
        public void Die()
        {
            data.isDie = true;
            foreach (IInterruptible behaviour in GetComponentsInChildren<IInterruptible>())
                behaviour.Interrupt();
            HandleEvent(EEnemyAction.Die);
        }
        private void DropItemAndDisappear()
        {
            EventManager.DropStar(transform.position, data.metaData.star);
            if (data.dropItemType != EItem.None)
                EventManager.DropItem(data.dropItemType, transform.position);
            Disappear();
        }
        private IEnumerator Prepare(float delay)
        {
            Enable(false);
            yield return new WaitForSeconds(delay);
            Enable(true);
            movement.Move();
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
                if (bullet.gameObject.activeSelf && TakeDamage(bullet.data.dmg))
                    bullet.Disappear();
            }
        }
    }
}