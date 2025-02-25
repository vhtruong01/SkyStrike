using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
                x.onSubmit.AddListener(s =>
                {
                    if (float.TryParse(s, out float newValue))
                        OnValueChanged();
                    else x.text = value.x.ToString();
                });
                y.onSubmit.AddListener(s =>
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
                base.SetValue(value);
                x.text = value.x.ToString();
                y.text = value.y.ToString();
            }
        }
    }
}