using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class StringProperty : Property<string>
        {
            [SerializeField] private TMP_InputField x;

            public override void Awake()
            {
                base.Awake();
                x.onValueChanged.AddListener(s => OnValueChanged());
            }
            protected override void OnValueChanged()
            {
                value = x.text;
                onValueChanged.Invoke(value);
            }
        }
    }
}