using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class GameData<T> : MonoBehaviour, IGame
    {
        protected IRefreshable mainObject;
        public T metaData {  get; protected set; }

        public virtual void Awake()
            => mainObject = GetComponent<IRefreshable>();
        public void UpdateDataAndRefresh(T data)
        {
            metaData = data;
            SetData(data);
            mainObject?.Refresh();
        }
        protected abstract void SetData(T metaData);
    }
}