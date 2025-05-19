using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Game
{
    public interface IRenderer
    {
        public Color color { get; set; }
        public void SetSprite(Sprite sprite);
        public void SetAlpha(float alpha);
    }
    public class RendererComponent : MonoBehaviour
    {
        private IRenderer rendererElement;
        public Color color
        {
            get => rendererElement.color;
            set => rendererElement.color = value;
        }
        public Material material { get; private set; }

        public void Init()
        {
            var image = GetComponent<Image>();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var tmp = GetComponent<TextMeshProUGUI>();

            if (image != null)
            {
                material = image.material;
                rendererElement = new Img(image);
            }
            else if (spriteRenderer != null)
            {
                material = spriteRenderer.material;
                rendererElement = new Spr(spriteRenderer);
            }
            else if (tmp != null)
                rendererElement = new Txt(tmp);
        }
        public void SetAlpha(float a)
            => rendererElement.SetAlpha(a);
        public void SetSprite(Sprite sprite)
            => rendererElement.SetSprite(sprite);

        private class Img : IRenderer
        {
            private Image img;
            public Color color
            {
                get => img.color;
                set => img.color = value;
            }

            public Img(Image img) => this.img = img;
            public void SetSprite(Sprite sprite)
                => img.sprite = sprite;
            public void SetAlpha(float alpha)
                => img.color = img.color.ChangeAlpha(alpha);
        }

        private class Spr : IRenderer
        {
            private SpriteRenderer spr;
            public Color color
            {
                get => spr.color;
                set => spr.color = value;
            }

            public Spr(SpriteRenderer spr) => this.spr = spr;
            public void SetSprite(Sprite sprite)
                => spr.sprite = sprite;
            public void SetAlpha(float alpha)
                => spr.color = spr.color.ChangeAlpha(alpha);
        }

        private class Txt : IRenderer
        {
            private TextMeshProUGUI txt;
            public Color color
            {
                get => txt.color;
                set => txt.color = value;
            }

            public Txt(TextMeshProUGUI txt) => this.txt = txt;
            public void SetSprite(Sprite sprite) { }
            public void SetAlpha(float alpha)
                => txt.color = txt.color.ChangeAlpha(alpha);
        }
    }
}