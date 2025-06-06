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
        public void Play();
        public bool IsPlaying();
        public void Pause();
        public void Stop();
        public void Restart();
        public void Kill();
        public IAnimation SetLoopType(ELoopType loopType);
        public IAnimation SetDelay(float delay);
        public IAnimation SetDuration(float duration, float delay = 0);
        public IAnimation SetStartedAction(UnityAction startedAction);
        public IAnimation SetStoppedAction(UnityAction stoppedAction);
        public IAnimation SetFinishedAction(UnityAction finishedAction);
    }
    public abstract class SimpleAnimation : MonoBehaviour, IAnimation
    {
        [field: SerializeField] public EAnimationType type { get; private set; }
        [SerializeField] protected bool autoPlay;
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

        public virtual void Awake()
            => InitLoop();
        private void InitLoop()
        {
            bool isLoop = loopType == ELoopType.YoyoLoop || loopType == ELoopType.RestartLoop;
            isYoyo = loopType == ELoopType.YoyoLoop || loopType == ELoopType.TwoDir;
            loops = isLoop ? -1 : (isYoyo ? 2 : 1);
            isDirty = true;
        }
        public IAnimation SetLoopType(ELoopType loopType)
        {
            if (this.loopType != loopType)
            {
                this.loopType = loopType;
                InitLoop();
            }
            return this;
        }
        public void OnEnable()
            => SetDefault();
        public void Start()
        {
            if (autoPlay)
                Restart();
        }
        public void OnDisable()
            => Pause();
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
            startedAction?.Invoke();
            if (tweener == null || !tweener.IsPlaying())
                StartAnimation();
            if (duration <= 0)
                finishedAction?.Invoke();
        }
        public void Stop()
        {
            SetDefault();
            tweener?.Pause();
            stoppedAction?.Invoke();
        }
        public bool IsPlaying()
        {
            if (tweener == null)
                return false;
            return tweener.IsPlaying();
        }
        public void Pause()
        {
            SetDefault();
            tweener?.Pause();
        }
        public void Restart()
        {
            startedAction?.Invoke();
            StartAnimation();
            if (duration <= 0)
                finishedAction?.Invoke();
        }
        private void StartAnimation()
        {
            if (duration > 0)
            {
                if (startVal != endVal && isDirty)
                    CreateTweener();
                tweener?.Restart();
            }
        }
        public void Kill()
        {
            isDirty = true;
            tweener?.Kill();
        }
        public virtual IAnimation SetDuration(float duration, float delay = 0)
        {
            if (duration >= 0 && this.duration != duration)
            {
                if (CanAnimate())
                {
                    this.duration = duration;
                    this.delay = Mathf.Max(delay, 0);
                    isDirty = duration > 0;
                }
                else this.duration = 0;
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
        protected virtual bool CanAnimate() => true;
        public void OnDestroy()
            => tweener = null;
    }
}