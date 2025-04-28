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
        public EAnimationType type { get; set; }
        public void Init();
        public void Play();
        public void Pause();
        public void Stop();
        public void Finish();
        public void Restart();
        public void Kill();
        public IAnimation SetDelay(float delay);
        public IAnimation SetDuration(float duration, float delay = 0);
        public IAnimation SetStartedAction(UnityAction startedAction);
        public IAnimation SetStoppedAction(UnityAction stoppedAction);
        public IAnimation SetFinishedAction(UnityAction finishedAction);
    }
    public abstract class SimpleAnimation : MonoBehaviour, IAnimation
    {
        [field: SerializeField] public EAnimationType type { get; set; }
        [SerializeField] private ELoopType loopType;
        [SerializeField] protected float startVal = 0;
        [SerializeField] protected float endVal = 1;
        [SerializeField] protected float duration = 1;
        [SerializeField] protected float delay = 0;
        protected bool isDirty = false;
        protected bool isYoyo = false;
        protected int loops = 1;
        protected Tweener tweener;
        protected UnityAction startedAction;
        protected UnityAction stoppedAction;
        protected UnityAction finishedAction;

        public virtual void Init()
        {
            if (endVal == 0 && endVal == startVal) endVal = 1;
            bool isLoop = loopType == ELoopType.YoyoLoop || loopType == ELoopType.RestartLoop;
            isYoyo = loopType == ELoopType.YoyoLoop || loopType == ELoopType.TwoDir;
            loops = isLoop ? -1 : (isYoyo ? 2 : 1);
            isDirty = true;
        }
        private void CreateTweener()
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
            tweener = DOTween.To(ChangeValue, startVal, endVal, duration)
                             .SetLoops(loops, isYoyo ? LoopType.Yoyo : LoopType.Restart)
                             .SetDelay(delay)
                             .SetAutoKill(false).SetEase(Ease.Linear)
                             .Pause();
            SetStartedAction(startedAction);
            SetStoppedAction(stoppedAction);
            SetFinishedAction(finishedAction);
            isDirty = false;
        }
        public virtual void Play()
        {
            if (tweener == null || !tweener.IsPlaying())
                Restart();
            else startedAction?.Invoke();
        }
        public virtual void Stop()
        {
            gameObject.SetActive(false);
            tweener?.Pause();
            stoppedAction?.Invoke();
        }
        public virtual void Pause() => tweener?.Pause();
        public virtual void Restart()
        {
            startedAction?.Invoke();
            if (duration > 0 && startVal != endVal && isDirty)
                CreateTweener();
            if (tweener != null)
            {
                gameObject.SetActive(true);
                tweener.Restart();
            }
        }
        public void Finish()
        {
            finishedAction?.Invoke();
            stoppedAction?.Invoke();
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
        public IAnimation SetFinishedAction(UnityAction action)
        {
            finishedAction = action;
            if (tweener != null)
                tweener.onComplete = Finish;
            return this;
        }
        protected abstract void ChangeValue(float value);
    }
}