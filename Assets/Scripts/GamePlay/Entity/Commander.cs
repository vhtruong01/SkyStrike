using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class Commander : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected GameObject entityObject;
        protected IEntityComponent[] entityComponents;
        public ISpawnable spawner { get; private set; }
        public IMoveable movement {  get; private set; }
        public IAnimator animator {  get; private set; }
        public IAnimator enti {  get; private set; }
        public IObject entity { get; set; }

        public virtual void Awake()
        {
            if (entityObject == null)
                entityObject = gameObject;
            entityComponents = entityObject.GetComponentsInChildren<IEntityComponent>(true);
            movement = GetComponentInChildren<IMoveable>(true);
            spawner = GetComponentInChildren<ISpawnable>(true);
            animator = GetComponentInChildren<IAnimator>(true);
            entity = GetComponentInChildren<IEntity>(true);
            SetData();
            foreach (var comp in entityComponents)
                comp.entity = entity;
            foreach (var comp in entityObject.GetComponentsInChildren<IInitalizable>(true))
                comp.Init();
        }
        protected abstract void SetData();
        public abstract void Interrupt();
        public abstract void Init();
        public void InterruptAllComponents()
        {
            foreach (var comp in entityComponents)
                comp.Interrupt();
        }
    }
}