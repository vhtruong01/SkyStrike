using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Property<T> : MonoBehaviour
        {
            [SerializeField] protected TextMeshProUGUI titleTxt;
            [SerializeField] protected TMP_InputField x;
            protected T value;
            public UnityEvent<T> onValueChanged { get; private set; }

            public virtual void Awake()
            {
                onValueChanged = new();
            }
            public abstract void OnValueChanged();
            public void SetValue(T value)
            {
                x.text = value.ToString();
                this.value = value;
            }
            public void Bind(UnityAction<T> action) => onValueChanged.AddListener(action);
            public void Unbind()
            {
                onValueChanged.RemoveAllListeners();
                SetValue(default);
            }
            public void SetTitle(string title)
            {
                if (titleTxt != null)
                    titleTxt.text = title;
            }
        }
    }
}