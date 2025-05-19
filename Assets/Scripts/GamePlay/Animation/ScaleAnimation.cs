using UnityEngine;

namespace SkyStrike.Game
{
    [DisallowMultipleComponent]
    public class ScaleAnimation : SimpleAnimation
    {
        [SerializeField] private float spinSpeed = 0f;
        [field: SerializeField] protected override float delay { get; set; }
        [field: SerializeField] public bool pauseAnimationOnFinish { get; private set; }
        [field: SerializeField] protected override float startVal { get; set; } = 1;
        [field: SerializeField] protected override float endVal { get; set; } = 2;
        [field: SerializeField] protected override float duration { get; set; } = 2.5f;

        protected override void ChangeValue(float value)
        {
            transform.localScale = Vector3.one * value;
            if (spinSpeed != 0)
            {
                float z = transform.eulerAngles.z + spinSpeed * Time.deltaTime;
                transform.localEulerAngles = transform.eulerAngles.SetZ(z);
            }
        }
        protected override void SetDefault()
            => transform.localScale = Vector3.one;
    }
}