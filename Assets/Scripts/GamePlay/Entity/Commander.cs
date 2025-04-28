using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public enum EEntityAction
    {
        None = 0,
        Stand,
        Move,
        Attack,
        StopAttack,
        Defend,
        Unprotected,
        TakeDamage,
        Highlight,
        Arrive,
        Disappear,
        Die,
        //
        Invincible,
        ActiveLaser,
        ActiveMissile,
        ActiveDoubleBulletWeapon,
        ActiveTripleBulletWeapon,
        //
        Win,
        Lose,
    }
    public interface ICommander : IEntityComponent
    {
        public void HandleEvent(EEntityAction action);
    }
    public abstract class Commander : MonoBehaviour, ICommander
    {
        protected readonly Dictionary<EEntityAction, UnityAction> entityEvents = new();
        protected IEntityComponent[] entityComponents;
        protected ISpawnable spawner;
        protected IMoveable movement;
        protected IAnimator animator;
        public IEntity entity { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public virtual void Awake()
        {
            movement = GetComponentInChildren<IMoveable>(true);
            spawner = GetComponentInChildren<ISpawnable>(true);
            entity = GetComponentInChildren<IEntity>(true);
            animator = GetComponentInChildren<IAnimator>(true);
            entityComponents = GetComponentsInChildren<IEntityComponent>(true);
            SetData();
            foreach (var comp in entityComponents)
            {
                comp.entity = entity;
                comp.notifyAction = HandleEvent;
                comp.Init();
            }
        }
        protected abstract void SetData();
        public abstract void Init();
        public virtual void Interrupt()
            => StopAllCoroutines();
        public void HandleEvent(EEntityAction action)
        {
            if (entityEvents.TryGetValue(action, out var evt))
                evt.Invoke();
        }
        public void InterruptAllComponents()
        {
            foreach (var comp in entityComponents)
                comp.Interrupt();
        }
        public virtual void OnDisable()
            => InterruptAllComponents();
    }
}