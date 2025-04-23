using UnityEngine.Events;

namespace SkyStrike.Game
{
    public abstract class EnemyAnimation
    {
        protected bool isLoop = false;
        protected float elapedTime;
        protected bool isActive;
        protected bool isCancelWhenFinish;
        protected UnityAction finishedAction;
        protected UnityAction startedAction;
        protected UnityAction stoppedAction;

        public EnemyAnimation SetLoop(bool isLoop)
        {
            this.isLoop = isLoop;
            return this;
        }
        public EnemyAnimation SetFinishedAction(UnityAction finishedAction)
        {
            this.finishedAction = finishedAction;
            return this;
        }
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
        public EnemyAnimation SetCancelWhenFinished(bool b)
        {
            isCancelWhenFinish = b;
            return this;
        }
        public void Start()
        {
            if (isActive) return;
            isActive = true;
            Reset();
            startedAction?.Invoke();
        }
        public void Stop()
        {
            isActive = false;
            stoppedAction?.Invoke();
        }
        protected abstract void Reset();
        public abstract void Animate(float deltaTime);
    }
}