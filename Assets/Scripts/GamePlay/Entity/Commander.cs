using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public interface ICommander : IEntityComponent
    {
        public void HandleEvent(EEntityAction action);
    }
    public abstract class Commander: MonoBehaviour, ICommander
    {
        protected IEntityComponent[] entityComponents;
        protected ISpawnable spawner;
        protected IMoveable movement;
        protected IEntity entity;
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public virtual void Awake()
        {
            movement = GetComponent<IMoveable>();
            spawner = GetComponent<ISpawnable>();
            entity = GetComponent<IEntity>();
            entityComponents = GetComponentsInChildren<IEntityComponent>();
            foreach (var comp in entityComponents)
                comp.notifyAction = HandleEvent;
        }
        public void InterruptAllComponents()
        {
            foreach (var comp in entityComponents)
                comp.Interrupt();
        }
        public abstract void HandleEvent(EEntityAction action);
        public abstract void Interrupt();
    }
}