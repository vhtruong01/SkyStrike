using UnityEngine.Events;

namespace SkyStrike.Game
{
    public enum EAnimationDirection
    {
        LR,
        RL,
        LRL,
        RLR
    }
    public enum EAnimationType
    {
        None = 0,
        Weapon,
        Engine,
        Shield,
        Damage,
        HighLight,
        Destruction
    }
    public interface IAnimator : IEntityComponent
    {
        public IEntityAnimation SetTrigger(EAnimationType type);
    }
    public interface IEntityAnimation
    {
        public void Start();
        public void Stop();
        public void ResetAndStart();
        public void Animate(float deltaTime);
        public IEntityAnimation Reset();
        public IEntityAnimation SetFinishedAction(UnityAction finishedAction);
        public IEntityAnimation SetLoop(bool isLoop);
        public IEntityAnimation SetTotalTime(float totalTime);
        public IEntityAnimation SetInterval(float interval);
        public IEntityAnimation SetDirection(EAnimationDirection direction);
        public IEntityAnimation PauseAnimationOnComplete(bool isPause);
    }
}