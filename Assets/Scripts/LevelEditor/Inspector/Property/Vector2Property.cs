using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Vector2Property : Property<Vector2>
        {
            [SerializeField] private bool isUseWorldUnit;
            [SerializeField] private bool isWholeNumber;
            [SerializeField] private TMP_InputField x;
            [SerializeField] private TMP_InputField y;
            private float unit = 1;

            public void Awake()
            {
                if (isUseWorldUnit) unit = EditorSetting.WORLD_UNIT;
                x.text = "0";
                x.onValueChanged.AddListener(s => OnValueChanged());
                x.onSubmit.AddListener(s => CheckValue(x, value.x));
                y.text = "0";
                y.onValueChanged.AddListener(s => OnValueChanged());
                y.onSubmit.AddListener(s => CheckValue(y, value.y));
            }
            protected override void OnValueChanged()
            {
                if (!float.TryParse(x.text, out float newX) || (isWholeNumber && newX < 0)) return;
                if (!float.TryParse(y.text, out float newY) || (isWholeNumber && newY < 0)) return;
                newX /= unit;
                newY /= unit;
                if (Mathf.Abs(value.x - newX) < 0.001f && Mathf.Abs(value.y - newY) < 0.001f) return;
                value.Set(newX, newY);
                onValueChanged.Invoke(value);
            }
            public override void SetValue(Vector2 value)
            {
                base.SetValue(value);
                x.text = (Mathf.Round(value.x * unit * 1000) / 1000).ToString();
                y.text = (Mathf.Round(value.y * unit * 1000) / 1000).ToString();
            }
            private void CheckValue(TMP_InputField field, float defaultVal)
            {
                if (!float.TryParse(field.text, out float val) || (isWholeNumber && val < 0))
                    field.text = (defaultVal * unit).ToString();
            }
            public void OnDisable()
            {
                CheckValue(x, value.x);
                CheckValue(y, value.y);
            }
        }
    }
}