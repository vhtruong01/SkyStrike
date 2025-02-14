using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class Vector2Property : MonoBehaviour
        {
            [SerializeField] private TextMeshProUGUI titleTxt;
            [SerializeField] private TMP_InputField x;
            [SerializeField] private TMP_InputField y;
            private Vector2 _value;
            public UnityEvent<Vector2> onSetValue {  get; private set; }

            public void Awake()
            {
                x.text = "0";
                y.text = "0";
                onSetValue = new();
            }
            public Vector2 value
            {
                get => _value;
                set
                {
                    if (_value.x != value.x) x.text = value.x.ToString();
                    if (_value.y != value.y) y.text = value.y.ToString();
                    _value = value;
                    onSetValue.Invoke(_value);
                }
            }
            public void SetTitle(string title)
            {
                if (titleTxt != null)
                    titleTxt.text = title;
            }
        }
    }
}