using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : Menu
        {
            [SerializeField] private UIGroupPool objectUIGroupPool;
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private Menu inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private Menu waveMenu;
            [SerializeField] private Button hierarchyMenuBtn;
            [SerializeField] private Menu hierarchyMenu;

            public override void Awake()
            {
                base.Awake();
                inspectorMenuBtn.onClick.AddListener(inspectorMenu.Expand);
                hierarchyMenuBtn.onClick.AddListener(hierarchyMenu.Expand);
                waveMenuBtn.onClick.AddListener(waveMenu.Expand);
                objectUIGroupPool.selectDataCall = EventManager.SelectObject;
            }
            public override void Init() { }
            private void DisplayObject(ObjectDataObserver objectData) => objectUIGroupPool.CreateItem(objectData);
            protected override void CreateObject(IEditorData data)
            {
                if (data is ObjectDataObserver objectData)
                    DisplayObject(objectData);
            }
            protected override void RemoveObject(IEditorData data) => objectUIGroupPool.RemoveItem(data);
            protected override void SelectObject(IEditorData data) => objectUIGroupPool.SelectItem(data);
            protected override void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                objectUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.GetList())
                    DisplayObject(objectData);
            }
        }
    }
}