using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Menu : MonoBehaviour, IMenu
        {
            [SerializeField] protected Button collapseBtn;

            public virtual void Awake()
            {
                if (collapseBtn != null)
                    collapseBtn.onClick.AddListener(Hide);
                EventManager.onCreateObject.AddListener(CreateObject);
                EventManager.onSelectObject.AddListener(SelectObject);
                EventManager.onRemoveObject.AddListener(RemoveObject);
                EventManager.onSelectWave.AddListener(SelectWave);
            }
            public abstract void Init();
            protected abstract void CreateObject(ObjectDataObserver data);
            protected abstract void RemoveObject(ObjectDataObserver data);
            protected abstract void SelectObject(ObjectDataObserver data);
            protected abstract void SelectWave(WaveDataObserver data);
            public virtual void Hide() => gameObject.SetActive(false);
            public virtual void Show() => gameObject.SetActive(true);
        }
    }
}