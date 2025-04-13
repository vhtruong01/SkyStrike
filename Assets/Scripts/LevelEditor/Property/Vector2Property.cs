using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Vector2Property : Property<Vector2>
        {
            [SerializeField] private bool isNonNegative;
            [SerializeField] private TMP_InputField x;
            [SerializeField] private TMP_InputField y;

            public void Awake()
            {
                x.text = "0";
                x.onValueChanged.AddListener(s => OnValueChanged());
                x.onSubmit.AddListener(s => CheckValue(x, value.x));
                y.text = "0";
                y.onValueChanged.AddListener(s => OnValueChanged());
                y.onSubmit.AddListener(s => CheckValue(y, value.y));
            }
            public override void OnValueChanged()
            {
                if (!float.TryParse(x.text, out float newX) || (isNonNegative && newX < 0)) return;
                if (!float.TryParse(y.text, out float newY) || (isNonNegative && newY < 0)) return;
                if (value.x == newX && value.y == newY) return;
                value.Set(newX, newY);
                onValueChanged.Invoke(value);
            }
            public override void SetValue(Vector2 value)
            {
                base.SetValue(value);
                x.SetTextWithoutNotify((Mathf.Round(value.x * 1000) / 1000).ToString());
                y.SetTextWithoutNotify((Mathf.Round(value.y * 1000) / 1000).ToString());
            }
            private void CheckValue(TMP_InputField field, float defaultVal)
            {
                if (!float.TryParse(field.text, out float val) || (isNonNegative && val < 0))
                    field.SetTextWithoutNotify(defaultVal.ToString());
            }
            public void OnDisable()
            {
                CheckValue(x, value.x);
                CheckValue(y, value.y);
            }
        }
    }
}