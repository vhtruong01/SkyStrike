using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent(typeof(PointItemList))]
    public class PathMenu : ScalableMenu, IPointerClickHandler
    {
        [SerializeField] private Button straightPointBtn;
        [SerializeField] private Button curvePointBtn;
        [SerializeField] private NormalButton addPointBtn;
        [SerializeField] private Button removeBtn;
        [SerializeField] private Button clearBtn;
        [SerializeField] private Button flipXBtn;
        [SerializeField] private UIGroup switchPointTypeBtn;
        [SerializeField] private PointMenu pointMenu;
        [SerializeField] private BulletSelectionMenu bulletSelectionMenu;
        private bool isEnabledAddPoint;
        private int pointType;
        private PointItemList group;
        private ObjectDataObserver objectData;

        public void OnEnable()
        {
            bulletSelectionMenu.Refresh();
            group.SelectNone();
        }
        protected override void Preprocess()
        {
            base.Preprocess();
            addPointBtn.AddListener(EnableAddPoint, () => isEnabledAddPoint);
            removeBtn.onClick.AddListener(RemovePoint);
            clearBtn.onClick.AddListener(Clear);
            flipXBtn.onClick.AddListener(FlipX);
            pointMenu.gameObject.SetActive(true);
            pointMenu.gameObject.SetActive(false);
            group = gameObject.GetComponent<PointItemList>();
            group.Init(DisplayPointInfo);
            group.screen = screen;
        }
        public override void Start()
        {
            base.Start();
            switchPointTypeBtn.AddListener(SelectPointType);
        }
        private void DisplayPointInfo(PointDataObserver pointData)
        {
            addPointBtn.SetValue(false);
            bulletSelectionMenu.SelectPoint(pointData);
            pointMenu.Display(pointData);
            pointMenu.Show();
            pointMenu.SetTitle("Point " + group.GetItemIndex(pointData));
        }
        private void RemovePoint()
        {
            bool isRemoved = group.RemovePoint();
            if (isRemoved) DisplayPointInfo(null);
        }
        private void Clear()
        {
            group.RemoveDataList();
            DisplayPointInfo(null);
        }
        private void FlipX() => group.FlipX();
        private void SelectPointType(int type) => pointType = type;
        private void EnableAddPoint(bool isEnabled) => isEnabledAddPoint = isEnabled;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isEnabledAddPoint && objectData != null && !isDragging)
                CreateNewPoint(screen.GetActualPosition(eventData.pointerCurrentRaycast.worldPosition));
        }
        private void CreateNewPoint(Vector2 pos)
        {
            PointDataObserver pointData = new();
            pointData.ChangePosition(pos);
            group.SetTypeToLastPoint(pointType == 0);
            group.CreateItemAndAddData(pointData);
        }
        protected override void CreateObject(ObjectDataObserver data) { }
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data == objectData) return;
            group.Clear();
            DisplayPointInfo(null);
            objectData = data;
            if (data != null)
                group.DisplayDataList(data.moveData);
        }
        protected override void RemoveObject(ObjectDataObserver data) => SelectObject(null);
        protected override void SelectWave(WaveDataObserver data) => SelectObject(null);
    }
}