using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class SpriteAnimation : SimpleAnimation
    {
        [SerializeField] private bool autoPlay;
        [SerializeField] private float interval = 0.075f;
        [SerializeField] private float spinSpeed = 0f;
        [field: SerializeField] protected override float delay { get; set; }
        [field: SerializeField] public bool pauseAnimationOnFinish { get; private set; }
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer spriteRenderer;
        private int spriteIndex;
        protected override float startVal { get; set; }
        protected override float endVal { get; set; }
        protected override float duration { get; set; }
        private int index
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

        public override void Awake()
        {
            base.Awake();
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetDefault();
            var temp = sprites;
            sprites = null;
            SetData(temp);
        }
        public void SetData(List<Sprite> sprites)
        {
            spriteIndex = -1;
            if (this.sprites != sprites && sprites != null)
            {
                isDirty = true;
                startVal = -0.1f;
                endVal = sprites.Count - (isYoyo ? 0.9f : 0.1f);
                SetDuration(interval * (sprites.Count - 1), delay);
            }
            this.sprites = sprites;
            if (autoPlay)
                Restart();
        }
        protected override void SetDefault()
        {
            if (pauseAnimationOnFinish && sprites.Count > 0)
                spriteRenderer.sprite = sprites[0];
            else spriteRenderer.sprite = null;
            transform.localRotation = Quaternion.identity;
        }
        public IAnimation SetInterval(float interval)
        {
            this.interval = interval;
            if (sprites != null)
                SetDuration(interval * (sprites.Count - 1));
            return this;
        }
        protected override void ChangeValue(float value)
        {
            index = index < value ? Mathf.FloorToInt(value) : Mathf.CeilToInt(value);
            if (spinSpeed != 0)
            {
                float z = transform.eulerAngles.z + spinSpeed * Time.deltaTime;
                transform.localEulerAngles = transform.eulerAngles.SetZ(z);
            }
        }
    }
}