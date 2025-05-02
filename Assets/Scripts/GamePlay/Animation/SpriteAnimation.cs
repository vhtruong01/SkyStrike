using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : SimpleAnimation
    {
        [SerializeField] protected bool autoPlay;
        [SerializeField] protected float interval = 0.075f;
        [field: SerializeField] protected override float delay { get; set; }
        [field: SerializeField] public bool pauseAnimationOnFinish { get; protected set; }
        [SerializeField] protected List<Sprite> sprites;
        protected SpriteRenderer spriteRenderer;
        protected int spriteIndex;
        protected override float startVal { get; set; }
        protected override float endVal { get; set; }
        protected override float duration { get; set; }
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

        public void OnEnable()
        {
            if (autoPlay)
                Restart();
        }
        public void OnDisable()
        {
            if (autoPlay)
                Pause();
        }
        public override void Init()
        {
            base.Init();
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (sprites != null && sprites.Count > 0)
                SetData(sprites);
            if (autoPlay)
                Play();
        }
        public void SetData(List<Sprite> sprites)
        {
            spriteIndex = -1;
            if (this.sprites != sprites && sprites != null)
                isDirty = true;
            this.sprites = sprites;
            SetDefault();
            startVal = -0.1f;
            endVal = sprites.Count - (isYoyo ? 0.9f : 0.1f);
            SetDuration(interval * (sprites.Count - 1), delay);
        }
        protected override void SetDefault()
        {
            if (pauseAnimationOnFinish && sprites.Count > 0)
                spriteRenderer.sprite = sprites[0];
            else spriteRenderer.sprite = null;
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