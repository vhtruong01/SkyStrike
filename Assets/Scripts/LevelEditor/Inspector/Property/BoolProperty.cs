using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class BoolProperty : Property<bool>
        {
            [SerializeField] private Toggle toggle;

            public override void Awake()
            {
                base.Awake();
                toggle.isOn = false;
                toggle.onValueChanged.AddListener(b =>OnValueChanged());
            }
            public override void OnValueChanged()
            {
                value = toggle.isOn;
                onValueChanged.Invoke(value);
            }
        }
    }
}