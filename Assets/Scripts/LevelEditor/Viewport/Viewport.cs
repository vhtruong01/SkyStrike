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
            [SerializeField] private Menu inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private Menu waveMenu;
            [SerializeField] private Button hierarchyMenuBtn;
            [SerializeField] private Menu hierarchyMenu;

            public void Awake()
            {
                inspectorMenuBtn.onClick.AddListener(inspectorMenu.Expand);
                waveMenuBtn.onClick.AddListener(waveMenu.Expand);
                hierarchyMenuBtn.onClick.AddListener(hierarchyMenu.Expand);
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
                objectUIGroupPool.CreateItem(objectData);
            }
        }
    }
}