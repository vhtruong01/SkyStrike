using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class DataObserver<T> : IData
        {
            public T data { get;private set; }
            public UnityEvent<T> onChangeData { get; private set; }

            public DataObserver() => onChangeData = new();
            public void OnlySetData(T data) => this.data = data;
            public void SetData(T data)
            {
                this.data = data;
                onChangeData.Invoke(this.data);
            }
            public void Bind(UnityAction<T> call)
            {
                onChangeData.AddListener(call);
                call.Invoke(data);
            }
            public void Unbind() => onChangeData.RemoveAllListeners();
            public void ResetData() => data = default;
        }
    }
}