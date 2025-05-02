using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MaterialAlphaAnimation : SimpleAnimation
    {
        protected SpriteRenderer spriteRenderer;
        [field: SerializeField] protected override float startVal { get; set; } = 0;
        [field: SerializeField] protected override float endVal { get; set; } = 1;
        [field: SerializeField] protected override float duration { get; set; } = 2.5f;
        protected override float delay { get; set; }

        public override void Init()
        {
            base.Init();
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetDefault();
        }
        public void SetData(Sprite sprite)
            => spriteRenderer.sprite = sprite;
        protected override void ChangeValue(float value)
            => spriteRenderer.material.SetFloat("_Alpha", value);
        protected override void SetDefault()
            => ChangeValue(0);
    }
}