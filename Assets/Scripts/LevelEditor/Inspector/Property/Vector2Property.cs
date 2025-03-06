using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Vector2Property : Property<Vector2>
        {
            [SerializeField] private TMP_InputField x;
            [SerializeField] private TMP_InputField y;

            public override void Awake()
            {
                base.Awake();
                x.text = "0";
                y.text = "0";
                x.onValueChanged.AddListener(s => OnValueChanged());
                y.onValueChanged.AddListener(s => OnValueChanged());
            }
            protected override void OnValueChanged()
            {
                if (x.text.Length == 0)
                {
                    x.text = "0";
                    x.caretPosition = 1;
                }
                if (y.text.Length == 0)
                {
                    y.text = "0";
                    y.caretPosition = 1;
                }
                if (!float.TryParse(x.text, out float newX))
                {
                    newX = value.x;
                    x.text = value.x.ToString();
                }
                if (!float.TryParse(y.text, out float newY))
                {
                    newY = value.y;
                    y.text = value.y.ToString();
                }
                if (value.x == newX & value.y == newY) return;
                value.Set(newX, newY);
                onValueChanged.Invoke(value);
            }
            public override void SetValue(Vector2 value)
            {
                value.Set(Mathf.Round(value.x * 1000) / 1000, Mathf.Round(value.y * 1000) / 1000);
                base.SetValue(value);
                x.text = value.x.ToString();
                y.text = value.y.ToString();
            }
        }
    }
}