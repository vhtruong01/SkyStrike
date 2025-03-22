using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class Menu : MonoBehaviour
        {
            [SerializeField] protected Button collapseBtn;

            public virtual void Awake()
            {
                if (collapseBtn != null)
                    collapseBtn.onClick.AddListener(Collapse);
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
            public virtual void Collapse() => gameObject.SetActive(false);
            public virtual void Expand() => gameObject.SetActive(true); 
        }
    }
}