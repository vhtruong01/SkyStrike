using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ValueChangeAnimation : EnemyAnimation
    {
        protected float startVal;
        protected float endVal;
        protected UnityAction<float> setValueAction;

        public ValueChangeAnimation(UnityAction<float> setVal, float startVal, float endVal, float totalTime)
        {
            this.startVal = startVal;
            this.endVal = endVal;
            this.totalTime = totalTime;
            setValueAction = setVal;
        }
        public override void Animate(float deltaTime)
        {
            //dir
            if (!isActive) return;
            elapedTime += deltaTime;
            setValueAction.Invoke(Mathf.Lerp(startVal, endVal, elapedTime / totalTime));
            if (elapedTime >= totalTime)
            {
                if (isLoop) Reset();
                else
                {
                    isActive = false;
                    if (!isPauseAnimationOnComplete)
                        Stop();
                }
                finishedAction?.Invoke();
            }
        }
        public override IEntityAnimation Reset()
        {
            elapedTime = 0;
            return this;
        }
    }
}