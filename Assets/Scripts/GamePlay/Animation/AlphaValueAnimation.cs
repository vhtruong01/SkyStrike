 using UnityEngine;

namespace SkyStrike.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RendererComponent))]
    public sealed class AlphaValueAnimation : SimpleAnimation
    {
        private RendererComponent rendererComp;
        [field: SerializeField] protected override float startVal { get; set; } = 0;
        [field: SerializeField] protected override float endVal { get; set; } = 1;
        [field: SerializeField] protected override float duration { get; set; } = 2.5f;
        protected override float delay { get; set; }

        public override void Awake()
        {
            base.Awake();
            rendererComp = GetComponent<RendererComponent>();
            rendererComp.Init();
        }
        public void SetData(Sprite sprite)
            => rendererComp.SetSprite(sprite);
        protected override void ChangeValue(float value)
            => rendererComp.SetAlpha(value);
        protected override void SetDefault()
            => ChangeValue(0);
    }
}