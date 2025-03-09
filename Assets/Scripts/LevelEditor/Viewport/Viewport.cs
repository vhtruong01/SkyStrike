using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : MonoBehaviour
        {
            [SerializeField] private UIGroupPool objectUIGroupPool;
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private GameObject inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private GameObject waveMenu;
            [SerializeField] private Button addObjectMenuBtn;
            [SerializeField] private GameObject addObjectMenu;

            public void Awake()
            {
                inspectorMenuBtn.onClick.AddListener(() => inspectorMenu.SetActive(true));
                waveMenuBtn.onClick.AddListener(() => waveMenu.SetActive(true));
                addObjectMenuBtn.onClick.AddListener(() => addObjectMenu.SetActive(true));
                EventManager.onCreateObject.AddListener(CreateObject);
                EventManager.onSelectWave.AddListener(SelectWave);
                EventManager.onSelectObject.AddListener(SelectObject);
                objectUIGroupPool.selectDataCall = EventManager.SelectObject;
            }
            public void CreateObject(IEditorData data)
            {
                var objectData = data as ObjectDataObserver;
                if (objectData != null)
                    DisplayObject(objectData);
            }
            public void SelectObject(IEditorData data) => objectUIGroupPool.SelectItem(data);
            public void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                objectUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.objectList)
                    DisplayObject(objectData);
            }
            private void DisplayObject(ObjectDataObserver objectData)
            {
                objectUIGroupPool.CreateItem(out ViewportItemUI obj,objectData);
                //
            }
        }
    }
}