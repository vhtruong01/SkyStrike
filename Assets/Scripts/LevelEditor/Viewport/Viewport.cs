using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : ScalableMenu
        {
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private Menu inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private Menu waveMenu;
            [SerializeField] private Button hierarchyMenuBtn;
            [SerializeField] private Menu addObjectMenu;
            private ViewportItemList viewportUIGroupPool;

            public override void Awake()
            {
                base.Awake();
                inspectorMenuBtn.onClick.AddListener(inspectorMenu.Show);
                hierarchyMenuBtn.onClick.AddListener(addObjectMenu.Show);
                waveMenuBtn.onClick.AddListener(waveMenu.Show);
            }
            public override void Init()
            {
                base.Init();
                EventManager.onSetRefObject.AddListener(SelectReferenceObject);
                viewportUIGroupPool = gameObject.GetComponent<ViewportItemList>();
                viewportUIGroupPool.Init(EventManager.SelectObject);
                viewportUIGroupPool.screen = screen;
            }
            public void SelectReferenceObject(ObjectDataObserver refData)
            {
                (viewportUIGroupPool.GetSelectedItem() as ViewportItemUI).SetRefObject(refData);
            }
            protected override void CreateObject(ObjectDataObserver data) => viewportUIGroupPool.CreateItem(data);
            protected override void RemoveObject(ObjectDataObserver data) => viewportUIGroupPool.RemoveItem(data);
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (data == null) viewportUIGroupPool.SelectNone();
                else
                {
                    viewportUIGroupPool.SelectItem(data);
                    SelectReferenceObject(data.refData);
                }
            }
            protected override void SelectWave(WaveDataObserver data)
            {
                viewportUIGroupPool.Clear();
                data.GetList(out var dataList);
                foreach (var objectData in dataList)
                    CreateObject(objectData);
            }
            public override void Restore()
            {
                base.Restore();
                Show();
            }
        }
    }
}