using TMPro;
using UnityEngine;

namespace SkyStrike.Editor
{
    public class FloatProperty : Property<float>
    {
        [SerializeField] private bool isNonNegative;
        [SerializeField] private TMP_InputField x;

        public void Awake()
        {
            x.onValueChanged.AddListener(s => OnValueChanged());
            x.onSubmit.AddListener(s => CheckValue());
            Refresh();
        }
        public override void OnValueChanged()
        {
            if (float.TryParse(x.text, out float newX) && value != newX && (!isNonNegative || newX >= 0))
            {
                if (value == newX) return;
                value = newX;
                onValueChanged.Invoke(value);
            }
        }
        public override void Refresh()
           => x.SetTextWithoutNotify((Mathf.Round(value * 1000) / 1000).ToString());
        private void CheckValue()
        {
            if (!float.TryParse(x.text, out float newX) || (isNonNegative && newX < 0))
                x.SetTextWithoutNotify(value.ToString());
        }
        public void OnDisable() => CheckValue();
    }
}