using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Property<T> : MonoBehaviour
        {
            protected T value;
            public UnityEvent<T> onValueChanged { get; protected set; }

            public virtual void Awake()
            {
                onValueChanged = new();
            }
            public virtual void SetValue(T value)
            {
                this.value = value;
            }
            public abstract void OnValueChanged();
            public virtual void Bind(UnityAction<T> action) => onValueChanged.AddListener(action);
            public virtual void Unbind()
            {
                onValueChanged.RemoveAllListeners();
                SetValue(default);
            }
        }
    }
}