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
                x.onValueChanged.AddListener(s =>
                {
                    if (float.TryParse(s, out float newValue))
                        OnValueChanged();
                    else x.text = value.x.ToString();
                });
                y.onValueChanged.AddListener(s =>
                {
                    if (float.TryParse(s, out float newValue))
                        OnValueChanged();
                    else y.text = value.y.ToString();
                });
            }
            public override void OnValueChanged()
            {
                value.Set(float.TryParse(x.text, out float newX) ? newX : value.x,
                          float.TryParse(y.text, out float newY) ? newY : value.y);
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