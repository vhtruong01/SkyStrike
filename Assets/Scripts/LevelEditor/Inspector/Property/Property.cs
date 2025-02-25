using System;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Property<T> : MonoBehaviour
        {
            protected T value;
            protected DataObserver<T> dataObserver;
            public UnityEvent<T> onValueChanged { get; protected set; }

            public virtual void Awake()
            {
                onValueChanged = new();
            }
            public virtual void SetValue(T value) => this.value = value;
            public abstract void OnValueChanged();
            public virtual void Bind(UnityAction<T> action) => onValueChanged.AddListener(action);
            public virtual void Bind(DataObserver<T> dataObserver)
            {
                if (this.dataObserver != null)
                    throw new Exception("only one observer data");
                this.dataObserver = dataObserver;
                this.dataObserver.Bind(SetValue);
                onValueChanged.AddListener(this.dataObserver.SetData);
            } 
            public virtual void Unbind()
            {
                dataObserver?.Unbind(SetValue);
                dataObserver = null;
                onValueChanged.RemoveAllListeners();
                SetValue(default);
            }
        }
    }
}