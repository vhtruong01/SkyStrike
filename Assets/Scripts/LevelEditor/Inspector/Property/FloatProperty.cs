using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class FloatProperty : Property<float>
        {
            [SerializeField] private bool isUseWorldUnit;
            [SerializeField] private bool isWholeNumber;
            [SerializeField] private TMP_InputField x;
            private float unit = 1;

            public void Awake()
            {
                if (isUseWorldUnit) unit = EditorSetting.WORLD_UNIT;
                x.text = "0";
                x.onValueChanged.AddListener(s => OnValueChanged());
                x.onSubmit.AddListener(s => CheckValue());
            }
            protected override void OnValueChanged()
            {
                if (float.TryParse(x.text, out float newX) && value != newX && (!isWholeNumber || newX >= 0))
                {
                    value = newX / unit;
                    onValueChanged.Invoke(value);
                }
            }
            public override void SetValue(float value)
            {
                base.SetValue(value);
                x.text = (Mathf.Round(value * unit * 1000) / 1000).ToString();
            }
            private void CheckValue()
            {
                if (!float.TryParse(x.text, out float newX) || (isWholeNumber && newX < 0))
                    x.text = (value * unit).ToString();
            }
            public void OnDisable() => CheckValue();
        }
    }
}