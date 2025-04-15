using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class StringProperty : Property<string>
        {
            [SerializeField] private TMP_InputField x;

            public void Awake()
            {
                x.onValueChanged.AddListener(s => OnValueChanged());
            }
            public override void OnValueChanged()
            {
                value = x.text;
                onValueChanged.Invoke(value);
            }
            public override void Refresh()
                => x.SetTextWithoutNotify(value);
        }
    }
}