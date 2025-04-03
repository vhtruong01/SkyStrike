using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class IntProperty : Property<int>
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
                if (int.TryParse(x.text, out int newX) && value != newX && (!isWholeNumber || newX >= 0))
                {
                    value = newX;
                    onValueChanged.Invoke(value);
                }
            }
            public override void SetValue(int value)
            {
                base.SetValue(value);
                x.SetTextWithoutNotify(value.ToString());
            }
            private void CheckValue()
            {
                if (!int.TryParse(x.text, out int newX) || (isWholeNumber && newX < 0))
                    x.SetTextWithoutNotify(value.ToString());
            }
            public void OnDisable() => CheckValue();
        }
    }
}