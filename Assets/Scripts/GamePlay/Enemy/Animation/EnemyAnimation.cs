using UnityEngine.Events;

namespace SkyStrike.Game
{
    public abstract class EnemyAnimation : IEntityAnimation
    {
        protected bool isLoop;
        protected bool isActive;
        protected bool isPauseAnimationOnComplete;
        protected float elapedTime;
        protected float totalTime;
        protected float interval;
        protected EAnimationDirection direction;
        protected UnityAction finishedAction;
        protected UnityAction startedAction;
        protected UnityAction stoppedAction;

        public EnemyAnimation SetStartedAction(UnityAction startedAction)
        {
            this.startedAction = startedAction;
            return this;
        }
        public EnemyAnimation SetStoppedAction(UnityAction stoppedAction)
        {
            this.stoppedAction = stoppedAction;
            return this;
        }
        public virtual IEntityAnimation SetTotalTime(float totalTime)
        {
            this.totalTime = totalTime;
            return this;
        }
        public virtual IEntityAnimation SetInterval(float interval)
        {
            this.interval = interval;
            return this;
        }
        public IEntityAnimation SetLoop(bool isLoop)
        {
            this.isLoop = isLoop;
            return this;
        }
        public IEntityAnimation SetFinishedAction(UnityAction finishedAction)
        {
            this.finishedAction = finishedAction;
            return this;
        }
        public IEntityAnimation PauseAnimationOnComplete(bool isPause)
        {
            isPauseAnimationOnComplete = isPause;
            return this;
        }
        public IEntityAnimation SetDirection(EAnimationDirection direction)
        {
            this.direction = direction;
            return this;
        }
        public abstract IEntityAnimation Reset();
        public void Start()
        {
            if (isActive) return;
            ResetAndStart();
        }
        public void Stop()
        {
            isActive = false;
            stoppedAction?.Invoke();
        }
        public void ResetAndStart()
        {
            isActive = true;
            _ = Reset();
            startedAction?.Invoke();
        }
        public abstract void Animate(float deltaTime);
    }
}