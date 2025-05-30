using UnityEngine;

namespace SkyStrike.Game
{
    [DisallowMultipleComponent]
    public abstract class GameData<T, K> : MonoBehaviour where T : IMetaData where K : IEventData
    {
        public T metaData { get; protected set; }
        protected IRefreshable mainObject;

        public void Awake()
            => mainObject = GetComponentInChildren<IRefreshable>();
        public void SetData(K eventData)
        {
            ChangeData(eventData);
            mainObject.Refresh();
        }
        protected abstract void ChangeData(K eventData);
    }
}