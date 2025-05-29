using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RendererComponent))]
    public sealed class SpriteAnimation : SimpleAnimation
    {
        [SerializeField] private float interval = 0.075f;
        [SerializeField] private float spinSpeed = 0f;
        [field: SerializeField] protected override float delay { get; set; }
        [field: SerializeField] public bool pauseAnimationOnFinish { get; private set; }
        [SerializeField] private List<Sprite> sprites;
        private RendererComponent rendererComponent;
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
                    rendererComponent.SetSprite(sprites[spriteIndex]);
                }
            }
        }

        public override void Awake()
        {
            base.Awake();
            rendererComponent = GetComponent<RendererComponent>();
            rendererComponent.Init();
            var temp = sprites;
            sprites = null;
            SetData(temp);
        }
        public void SetData(List<Sprite> sprites)
        {
            spriteIndex = -1;
            if (sprites == null || sprites.Count == 0)
            {
                this.sprites = null;
                isDirty = false;
                SetDuration(0);
            }
            else if (this.sprites != sprites)
            {
                this.sprites = sprites;
                isDirty = true;
                startVal = -0.1f;
                endVal = sprites.Count - (isYoyo ? 0.9f : 0.1f);
                SetDuration(interval * (sprites.Count - 1), delay);
            }
            if (autoPlay)
                Restart();
        }
        protected override void SetDefault()
        {
            transform.localRotation = Quaternion.identity;
            if (sprites == null || sprites.Count == 0 || !pauseAnimationOnFinish)
                rendererComponent.SetSprite(null);
            else rendererComponent.SetSprite(sprites[0]);
        }
        public IAnimation SetInterval(float interval)
        {
            this.interval = interval;
            if (sprites != null)
                SetDuration(interval * (sprites.Count - 1));
            return this;
        }
        protected override bool CanAnimate()
            => sprites != null && sprites.Count > 0;
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