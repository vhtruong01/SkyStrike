using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent(typeof(ViewportItemList))]
    public class Viewport : ScalableMenu
    {
        [SerializeField] private Button inspectorMenuBtn;
        [SerializeField] private Menu inspectorMenu;
        [SerializeField] private Button waveMenuBtn;
        [SerializeField] private Menu waveMenu;
        [SerializeField] private Button hierarchyMenuBtn;
        [SerializeField] private Menu addObjectMenu;
        private ViewportItemList viewportUIGroupPool;

        protected override void Preprocess()
        {
            base.Preprocess();
            EventManager.onSetRefObject.AddListener(SelectReferenceObject);
            inspectorMenuBtn.onClick.AddListener(inspectorMenu.Show);
            hierarchyMenuBtn.onClick.AddListener(addObjectMenu.Show);
            waveMenuBtn.onClick.AddListener(waveMenu.Show);
            viewportUIGroupPool = gameObject.GetComponent<ViewportItemList>();
            viewportUIGroupPool.Init(EventManager.SelectObject);
        }
        public override void Start()
        {
            base.Start();
            Show();
        }
        public void SelectReferenceObject(ObjectDataObserver refData)
            => (viewportUIGroupPool.GetSelectedItem() as ViewportItemUI).SetRefObject(refData);
        protected override void CreateObject(ObjectDataObserver data) => viewportUIGroupPool.CreateItem(data);
        protected override void RemoveObject(ObjectDataObserver data) => viewportUIGroupPool.RemoveItem(data);
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data != null)
            {
                viewportUIGroupPool.SelectItem(data);
                SelectReferenceObject(data.refData);
            }
            else viewportUIGroupPool.SelectNone();
        }
        protected override void SelectWave(WaveDataObserver data)
        {
            viewportUIGroupPool.Clear();
            data.GetList(out var dataList);
            foreach (var objectData in dataList)
                CreateObject(objectData);
        }
    }
}