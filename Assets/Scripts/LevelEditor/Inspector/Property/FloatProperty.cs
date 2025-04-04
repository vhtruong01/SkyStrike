using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class FloatProperty : Property<float>
        {
            [SerializeField] private bool isWholeNumber;
            [SerializeField] private TMP_InputField x;

            public void Awake()
            {
                x.text = "0";
                x.onValueChanged.AddListener(s => OnValueChanged());
                x.onSubmit.AddListener(s => CheckValue());
            }
            protected override void OnValueChanged()
            {
                if (float.TryParse(x.text, out float newX) && value != newX && (!isWholeNumber || newX >= 0))
                {
                    if (value == newX) return;
                    value = newX;
                    onValueChanged.Invoke(value);
                }
            }
            public override void SetValue(float value)
            {
                base.SetValue(value);
                x.SetTextWithoutNotify((Mathf.Round(value * 1000) / 1000).ToString());
            }
            private void CheckValue()
            {
                if (!float.TryParse(x.text, out float newX) || (isWholeNumber && newX < 0))
                    x.SetTextWithoutNotify(value.ToString());
            }
            public void OnDisable() => CheckValue();
        }
    }
}