using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class GameData<T, K> : MonoBehaviour where T : IMetaData where K : IEventData
    {
        [field: SerializeField] public T metaData { get; protected set; }
        protected IRefreshable mainObject;

        public virtual void Awake()
            => mainObject = GetComponentInChildren<IRefreshable>();
        public void SetData(K eventData)
        {
            ChangeData(eventData);
            mainObject.Refresh();
        }
        protected abstract void ChangeData(K eventData);
    }
}