using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class FloatProperty : Property<float>
        {
            [SerializeField] private TMP_InputField x;

            public override void Awake()
            {
                base.Awake();
                x.text = "0";
                x.onValueChanged.AddListener(s =>
                {
                    if (float.TryParse(s, out float newValue))
                        OnValueChanged();
                    else x.text = value.ToString();
                });
            }
            protected override void OnValueChanged()
            {
                float.TryParse(x.text, out float newX);
                if (value == newX) return;
                value = newX;
                onValueChanged.Invoke(value);
            }
            public override void SetValue(float value)
            {
                value = Mathf.Round(value * 1000) / 1000;
                base.SetValue(value);
                x.text = value.ToString();
            }
        }
    }
}