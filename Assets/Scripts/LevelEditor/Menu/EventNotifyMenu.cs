using UnityEngine;
namespace SkyStrike
{
    namespace Editor
    {
        public abstract class EventNotifyMenu : Menu
        {
            public override void Init()
            {
                EventManager.onCreateObject.AddListener(CreateObject);
                EventManager.onSelectObject.AddListener(SelectObject);
                EventManager.onRemoveObject.AddListener(RemoveObject);
                EventManager.onSelectWave.AddListener(SelectWave);
            }
            protected abstract void CreateObject(ObjectDataObserver data);
            protected abstract void RemoveObject(ObjectDataObserver data);
            protected abstract void SelectObject(ObjectDataObserver data);
            protected abstract void SelectWave(WaveDataObserver data);
        }
    }
}