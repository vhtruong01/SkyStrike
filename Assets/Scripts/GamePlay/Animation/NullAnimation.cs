using UnityEngine.Events;

namespace SkyStrike.Game
{
    public sealed class NullAnimation : IAnimation
    {
        private readonly static NullAnimation instance = new();
        public static NullAnimation Instance => instance;
        public EAnimationType type => EAnimationType.None;
        public bool isNull => true;
        private NullAnimation() { }
        public void Play() { }
        public void Kill() { }
        public void Pause() { }
        public void Stop() { }
        public void Restart() { }
        public IAnimation SetDelay(float delay) => this;
        public IAnimation SetDuration(float duration, float delay = 0) => this;
        public IAnimation SetStartedAction(UnityAction startedAction) => this;
        public IAnimation SetStoppedAction(UnityAction stoppedAction) => this;
        public IAnimation SetFinishedAction(UnityAction finishedAction) => this;
    }
}