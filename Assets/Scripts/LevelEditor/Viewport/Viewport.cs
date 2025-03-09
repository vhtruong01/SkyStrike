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
            private WaveDataObserver waveDataObserver;

            public void Awake()
            {
                inspectorMenuBtn.onClick.AddListener(() => inspectorMenu.SetActive(true));
                waveMenuBtn.onClick.AddListener(() => waveMenu.SetActive(true));
                addObjectMenuBtn.onClick.AddListener(() => addObjectMenu.SetActive(true));
                EventManager.onCreateObject.AddListener(CreateObject);
                EventManager.onSelectWave.AddListener(SelectWave);
            }
            public void Start()
            {
                objectUIGroupPool.selectDataCall = EventManager.SelectObject;
            }
            public void CreateObject(IEditorData data)
            {
                var objectData = (data as ObjectDataObserver).Clone();
                if (objectData == null) return;
                DisplayObject(objectData);
                waveDataObserver.AddObject(objectData);
            }
            public void SelectWave(IEditorData data)
            {
                waveDataObserver = data as WaveDataObserver;
                objectUIGroupPool.Clear();
                foreach(var objectData in waveDataObserver.objectList)
                    DisplayObject(objectData);
            }
            private void DisplayObject(ObjectDataObserver objectData)
            {
                objectUIGroupPool.CreateItem(out ViewportItemUI obj);
                obj.SetData(objectData);
            }
        }
    }
}