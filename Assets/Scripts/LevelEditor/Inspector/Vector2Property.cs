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
            private Vector2 value;
            public UnityEvent<Vector2> onValueChanged { get; private set; }

            public void Awake()
            {
                x.text = "0";
                y.text = "0";
                x.onSubmit.AddListener(s =>
                {
                    if (float.TryParse(s, out float newValue))
                        OnValueChanged();
                    else x.text = value.x.ToString();
                });
                y.onSubmit.AddListener(s =>
                {
                    if (float.TryParse(s, out float newValue))
                        OnValueChanged();
                    else y.text = value.y.ToString();
                });
                onValueChanged = new();
            }
            public void OnValueChanged()
            {
                value.Set(float.TryParse(x.text, out float newX) ? newX : value.x,
                          float.TryParse(y.text, out float newY) ? newY : value.y);
                onValueChanged.Invoke(value);
            }
            public void SetValue(Vector2 value)
            {
                x.text = value.x.ToString();
                y.text = value.y.ToString();
                this.value = value;
            }
            public void Bind(UnityAction<Vector2> action) => onValueChanged.AddListener(action);
            public void Unbind() => onValueChanged.RemoveAllListeners();
            public void SetTitle(string title)
            {
                if (titleTxt != null)
                    titleTxt.text = title;
            }
        }
    }
}