using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class BoolProperty : Property<bool>
        {
            [SerializeField] private Toggle toggle;
            protected UnityEvent<bool> onEnable = new();
            protected UnityEvent<bool> onDisable = new();

            public void Awake()
                => toggle.onValueChanged.AddListener(b => OnValueChanged());
            public void Start()
                => toggle.isOn = toggle.isOn;
            public void BindToOtherProperty(IProperty property, bool isSame = true)
            {
                if (isSame)
                    onEnable.AddListener(property.Display);
                else onDisable.AddListener(property.Display);
            }
            public override void OnValueChanged()
            {
                value = toggle.isOn;
                onValueChanged.Invoke(value);
                EnableOtherProperty(value);
            }
            private void EnableOtherProperty(bool isEnable)
            {
                onEnable.Invoke(isEnable);
                onDisable.Invoke(!isEnable);
            }
            public override void SetValue(bool value)
            {
                base.SetValue(value);
                toggle.SetIsOnWithoutNotify(value);
            }
            public void OnEnable()
                => EnableOtherProperty(true);
            public void OnDisable()
                => EnableOtherProperty(false);
        }
    }
}