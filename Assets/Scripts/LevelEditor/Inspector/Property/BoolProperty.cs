using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class BoolProperty : Property<bool>
        {
            [SerializeField] private Toggle toggle;

            public void Awake()
            {
                toggle.isOn = false;
                toggle.onValueChanged.AddListener(b => OnValueChanged());
            }
            protected override void OnValueChanged()
            {
                value = toggle.isOn;
                onValueChanged.Invoke(value);
            }
            public override void SetValue(bool value)
            {
                base.SetValue(value);
                toggle.isOn = value;
            }
        }
    }
}