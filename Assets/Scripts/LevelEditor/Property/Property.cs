using System;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Property<T> : MonoBehaviour, IProperty
        {
            protected T value;
            protected DataObserver<T> dataObserver;
            protected UnityEvent<T> onValueChanged = new();

            public abstract void OnValueChanged();
            public virtual void SetValue(T value) => this.value = value;
            public virtual void Bind(DataObserver<T> dataObserver, UnityAction<T> action = null)
            {
                if (this.dataObserver != null)
                    throw new Exception("only one observer data");
                this.dataObserver = dataObserver;
                this.dataObserver.Bind(SetValue);
                if (action != null)
                    onValueChanged.AddListener(action);
                else onValueChanged.AddListener(this.dataObserver.SetData);
            }
            public virtual void Unbind()
            {
                dataObserver?.Unbind(SetValue);
                dataObserver = null;
                onValueChanged.RemoveAllListeners();
                SetValue(default);
            }

            public void Display(bool isEnable)
                => gameObject.SetActive(isEnable);
        }
    }
}