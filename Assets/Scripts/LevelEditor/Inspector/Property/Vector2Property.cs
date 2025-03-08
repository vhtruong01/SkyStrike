using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Vector2Property : Property<Vector2>
        {
            [SerializeField] private bool isWholeNumber;
            [SerializeField] private TMP_InputField x;
            [SerializeField] private TMP_InputField y;

            public override void Awake()
            {
                base.Awake();
                x.text = "0";
                y.text = "0";
                x.onValueChanged.AddListener(s => OnValueChanged());
                x.onSubmit.AddListener(s => CheckValue(x, value.x));
                y.onValueChanged.AddListener(s => OnValueChanged());
                y.onSubmit.AddListener(s => CheckValue(y, value.y));
            }
            protected override void OnValueChanged()
            {
                if (!float.TryParse(x.text, out float newX) || (isWholeNumber && newX < 0)) return;
                if (!float.TryParse(y.text, out float newY) || (isWholeNumber && newY < 0)) return;
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
            private void CheckValue(TMP_InputField field, float defaultVal)
            {
                if (!float.TryParse(field.text, out float val) || (isWholeNumber && val < 0))
                    field.text = defaultVal.ToString();
            }
            public void OnDisable()
            {
                CheckValue(x, value.x);
                CheckValue(y, value.y);
            }
        }
    }
}