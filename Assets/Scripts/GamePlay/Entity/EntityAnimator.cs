using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public enum EAnimationType
    {
        None = 0,
        MainWeapon,
        Engine,
        Shield,
        Damaged,
        Highlight,
        Destruction,
        //
        Missile,
        Laser,
        DoubleBulletWeapon,
        TripleBulletWeapon,
        Invincibility
    }
    public interface IAnimator : IEntityComponent
    {
        public IAnimation SetTrigger(EAnimationType type);
        public abstract void OnDestroy();
    }
    public abstract class EntityAnimator : MonoBehaviour, IAnimator
    {
        private Dictionary<EAnimationType, IAnimation> animations;
        public IEntity entity { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public virtual void Init()
        {
            animations = new();
            foreach(var animation in GetComponentsInChildren<IAnimation>(true))
            {
                animations[animation.type] = animation;
                animation.Init();
            }
        }
        public virtual IAnimation SetTrigger(EAnimationType type)
        {
            if (animations.TryGetValue(type, out var animation))
                return animation;
            return null;
        }
        public void Interrupt()
        {
            if (animations == null) return;
            foreach (var animation in animations.Values)
                animation.Stop();
        }
        public void OnDestroy()
        {
            if (animations == null) return;
            foreach (var animation in animations.Values)
                animation.Kill();
        }
    }
}