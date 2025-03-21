using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : Menu
        {
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private Menu inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private Menu waveMenu;
            [SerializeField] private Button hierarchyMenuBtn;
            [SerializeField] private Menu hierarchyMenu;
            private ViewportItemList viewportUIGroupPool;

            public override void Awake()
            {
                base.Awake();
                inspectorMenuBtn.onClick.AddListener(inspectorMenu.Expand);
                hierarchyMenuBtn.onClick.AddListener(hierarchyMenu.Expand);
                waveMenuBtn.onClick.AddListener(waveMenu.Expand);
            }
            public override void Init()
            {
                viewportUIGroupPool = gameObject.GetComponent<ViewportItemList>();
                viewportUIGroupPool.Init(EventManager.SelectObject);
            }
            protected override void CreateObject(ObjectDataObserver data) => viewportUIGroupPool.CreateItem(data);
            protected override void RemoveObject(ObjectDataObserver data) => viewportUIGroupPool.RemoveItem(data);
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (data == null) viewportUIGroupPool.SelectNone();
                else viewportUIGroupPool.SelectItem(data);
            }
            protected override void SelectWave(WaveDataObserver data)
            {
                viewportUIGroupPool.Clear();
                foreach (var objectData in data.GetList())
                    CreateObject(objectData);
            }
        }
    }
}