using UnityEngine;

namespace SkyStrike.Game
{
    public class MaterialAlphaAnimation : SimpleAnimation
    {
        protected SpriteRenderer spriteRenderer;

        public override void Init()
        {
            base.Init();
            spriteRenderer = GetComponent<SpriteRenderer>();
            ChangeValue(0);
            SetStartedAction(() => spriteRenderer.gameObject.SetActive(true));
            SetStoppedAction(() => spriteRenderer.gameObject.SetActive(false));
        }
        public void SetData(Sprite sprite)
            => spriteRenderer.sprite = sprite;
        protected override void ChangeValue(float value)
            => spriteRenderer.material.SetFloat("_Alpha", value);
    }
}