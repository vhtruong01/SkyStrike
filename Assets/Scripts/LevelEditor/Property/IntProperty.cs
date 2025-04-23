using TMPro;
using UnityEngine;

namespace SkyStrike.Editor
{
    public class IntProperty : Property<int>
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
            if (int.TryParse(x.text, out int newX) && value != newX && (!isNonNegative || newX >= 0))
            {
                value = newX;
                onValueChanged.Invoke(value);
            }
        }
        public override void Refresh()
            => x.SetTextWithoutNotify(value.ToString());
        private void CheckValue()
        {
            if (!int.TryParse(x.text, out int newX) || (isNonNegative && newX < 0))
                x.SetTextWithoutNotify(value.ToString());
        }
        public void OnDisable() => CheckValue();
    }
}