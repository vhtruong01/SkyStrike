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
        private ViewportItemList group;

        protected override void Preprocess()
        {
            base.Preprocess();
            EventManager.onSetRefObject.AddListener(SelectReferenceObject);
            inspectorMenuBtn.onClick.AddListener(inspectorMenu.Show);
            hierarchyMenuBtn.onClick.AddListener(addObjectMenu.Show);
            waveMenuBtn.onClick.AddListener(waveMenu.Show);
            group = gameObject.GetComponent<ViewportItemList>();
            group.Init(EventManager.SelectObject);
        }
        public override void Start()
        {
            base.Start();
            Show();
        }
        public void SelectReferenceObject(ObjectDataObserver refData)
            => (group.GetSelectedItem() as ViewportItemUI).SetRefObject(refData);
        protected override void CreateObject(ObjectDataObserver data) => group.CreateItem(data);
        protected override void RemoveObject(ObjectDataObserver data) => group.RemoveItem(data);
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data != null)
            {
                group.SelectItem(data);
                SelectReferenceObject(data.refData);
            }
            else group.SelectNone();
        }
        protected override void SelectWave(WaveDataObserver data)
        {
            group.Clear();
            data.GetList(out var dataList);
            foreach (var objectData in dataList)
                CreateObject(objectData);
        }
    }
}