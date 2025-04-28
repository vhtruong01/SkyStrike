using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class SpriteAnimation : SimpleAnimation
    {
        [SerializeField] protected float interval = 0.075f;
        [SerializeField] protected List<Sprite> sprites;
        protected SpriteRenderer spriteRenderer;
        protected int spriteIndex;
        protected int index
        {
            get => spriteIndex;
            set
            {
                if (spriteIndex != value)
                {
                    spriteIndex = value;
                    spriteRenderer.sprite = sprites[spriteIndex];
                }
            }
        }

        public override void Init()
        {
            base.Init();
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetData(sprites);
            SetStartedAction(() => spriteRenderer.gameObject.SetActive(true));
            SetStoppedAction(() => spriteRenderer.gameObject.SetActive(false));
        }
        public void SetData(List<Sprite> sprites)
        {
            spriteIndex = -1;
            if (this.sprites != sprites && sprites != null)
                isDirty = true;
            this.sprites = sprites;
            spriteRenderer.sprite = null;
            startVal = -0.1f;
            endVal = sprites.Count - (isYoyo ? 0.9f : 0.1f);
            duration = interval * (sprites.Count - 1);
        }
        public IAnimation SetInterval(float interval)
        {
            this.interval = interval;
            if (sprites != null)
                SetDuration(interval * (sprites.Count - 1));
            return this;
        }
        protected override void ChangeValue(float value)
            => index = index < value ? Mathf.FloorToInt(value) : Mathf.CeilToInt(value);
    }
}