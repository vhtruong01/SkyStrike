using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ValueChangeAnimation : EnemyAnimation
    {
        protected float startVal;
        protected float endVal;
        protected float totalTime;
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
            if (!isActive) return;
            elapedTime += deltaTime;
            setValueAction.Invoke(Mathf.Lerp(startVal, endVal, elapedTime / totalTime));
            Debug.Log(Mathf.Lerp(startVal, endVal, elapedTime / totalTime));
            if (elapedTime >= totalTime)
            {
                if (isLoop) Reset();
                else
                {
                    isActive = false;
                    if (isCancelWhenFinish)
                        Stop();
                }
                finishedAction?.Invoke();
            }
        }
        protected override void Reset() 
            => elapedTime = 0;
    }
}