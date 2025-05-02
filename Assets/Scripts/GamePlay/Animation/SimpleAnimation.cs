using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public enum ELoopType
    {
        OneDir,
        TwoDir,
        RestartLoop,
        YoyoLoop
    }
    public interface IAnimation
    {
        public EAnimationType type { get; }
        public bool isNull { get; }
        public void Init();
        public void Play();
        public void Pause();
        public void Stop();
        public void Restart();
        public void Kill();
        public IAnimation SetDelay(float delay);
        public IAnimation SetDuration(float duration, float delay = 0);
        public IAnimation SetStartedAction(UnityAction startedAction);
        public IAnimation SetStoppedAction(UnityAction stoppedAction);
        public IAnimation SetFinishedAction(UnityAction finishedAction);
    }
    public abstract class SimpleAnimation : MonoBehaviour, IAnimation, IInitalizable
    {
        [field: SerializeField] public EAnimationType type { get; private set; }
        [SerializeField] private ELoopType loopType;
        protected bool isDirty = false;
        protected bool isYoyo = false;
        protected int loops = 1;
        protected Tweener tweener;
        protected UnityAction startedAction;
        protected UnityAction stoppedAction;
        protected UnityAction finishedAction;
        public bool isNull => false;
        protected abstract float delay { get; set; }
        protected abstract float startVal { get; set; }
        protected abstract float endVal { get; set; }
        protected abstract float duration { get; set; }

        public virtual void Init()
        {
            bool isLoop = loopType == ELoopType.YoyoLoop || loopType == ELoopType.RestartLoop;
            isYoyo = loopType == ELoopType.YoyoLoop || loopType == ELoopType.TwoDir;
            loops = isLoop ? -1 : (isYoyo ? 2 : 1);
            isDirty = true;
        }
        private void CreateTweener()
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
            isDirty = false;
            tweener = DOTween.To(ChangeValue, startVal, endVal, duration)
                             .Pause()
                             .SetLoops(loops, isYoyo ? LoopType.Yoyo : LoopType.Restart)
                             .SetDelay(delay)
                             .SetAutoKill(false).SetEase(Ease.Linear)
                             .OnComplete(() =>
                             {
                                 SetDefault();
                                 finishedAction?.Invoke();
                             });
        }
        public virtual void Play()
        {
            if (tweener == null || !tweener.IsPlaying())
                Restart();
            else startedAction?.Invoke();
        }
        public void Stop()
        {
            SetDefault();
            tweener?.Pause();
            stoppedAction?.Invoke();
        }
        public void Pause() => tweener?.Pause();
        public void Restart()
        {
            startedAction?.Invoke();
            if (duration > 0 && startVal != endVal && isDirty)
                CreateTweener();
            tweener?.Restart();
        }
        public void Kill()
        {
            isDirty = true;
            tweener?.Kill();
        }
        public IAnimation SetDuration(float duration, float delay = 0)
        {
            if (duration > 0 && this.duration != duration)
            {
                this.duration = duration;
                this.delay = Mathf.Max(delay, 0);
                isDirty = true;
            }
            return this;
        }
        public IAnimation SetDelay(float delay)
        {
            if (this.delay != delay)
            {
                this.delay = Mathf.Max(delay, 0);
                isDirty = true;
            }
            return this;
        }
        public IAnimation SetStartedAction(UnityAction action)
        {
            startedAction = action;
            return this;
        }
        public IAnimation SetStoppedAction(UnityAction action)
        {
            stoppedAction = action;
            return this;
        }
        public IAnimation SetFinishedAction(UnityAction finishedAction)
        {
            this.finishedAction = finishedAction;
            return this;
        }
        protected abstract void ChangeValue(float value);
        protected abstract void SetDefault();
    }
}