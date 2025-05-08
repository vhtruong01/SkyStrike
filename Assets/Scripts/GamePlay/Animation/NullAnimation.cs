using UnityEngine.Events;

namespace SkyStrike.Game
{
    public sealed class NullAnimation : IAnimation
    {
        private readonly string msg;
        public EAnimationType type => EAnimationType.None;
        public bool isNull => true;
        public NullAnimation() { }
        public NullAnimation(string msg) => this.msg = msg;
        public void Play() => PrintMessage();
        public void Init() { }
        public void Kill() { }
        public void Pause() { }
        public void Restart() { }
        public IAnimation SetDelay(float delay) => this;
        public IAnimation SetDuration(float duration, float delay = 0) => this;
        public IAnimation SetStartedAction(UnityAction startedAction) => this;
        public IAnimation SetStoppedAction(UnityAction stoppedAction) => this;
        public IAnimation SetFinishedAction(UnityAction finishedAction) => this;
        public void Stop() { }
        private void PrintMessage()
        {
            if (!string.IsNullOrEmpty(msg))
                UnityEngine.Debug.Log(msg);
        }
    }
}