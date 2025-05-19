using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum EAnimationType
    {
        None = 0,
        Damaged,
        Highlight,
        Destruction,
        MainWeapon,
        Engine,
        Shield,
        Invincibility,
    }
    public interface IAnimator
    {
        public IAnimation GetAnimation(EAnimationType type);
        public abstract void OnDestroy();
    }
    public class EntityAnimator : MonoBehaviour, IAnimator, IEntityComponent
    {
        private Dictionary<EAnimationType, IAnimation> animations;
        public IObject entity { get; set; }

        public virtual void Init()
        {
            animations = new();
            foreach (var animation in GetComponentsInChildren<IAnimation>(true))
                animations[animation.type] = animation;
        }
        public virtual IAnimation GetAnimation(EAnimationType type)
        {
            if (animations.TryGetValue(type, out var animation))
                return animation;
            return NullAnimation.Instance;
        }
        public void Interrupt()
        {
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