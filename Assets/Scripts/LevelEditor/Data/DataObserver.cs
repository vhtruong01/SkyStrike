using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class DataObserver<T> : IData
        {
            private T _data;
            public T data
            {
                get => _data;
                set
                {
                    _data = value;
                    OnValueChanged(_data);
                }
            }
            public UnityEvent<T> onChangeData { get; private set; }

            public DataObserver() => onChangeData = new();
            public void OnlySetData(T data) => this.data = data;
            public void Bind(UnityAction<T> call)
            {
                onChangeData.AddListener(call);
                call.Invoke(data);
            }
            public void Unbind() => onChangeData.RemoveAllListeners();
            public void OnValueChanged(T data) => onChangeData.Invoke(data);
            public void ResetData() => data = default;
        }
    }
}