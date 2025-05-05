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
        private PointItemList pointItemList;
        private ObjectDataObserver objectDataObserver;

        public void OnEnable()
            => bulletSelectionMenu.Refresh();
        protected override void Preprocess()
        {
            base.Preprocess();
            addPointBtn.AddListener(EnableAddPoint, () => isEnabledAddPoint);
            removeBtn.onClick.AddListener(RemovePoint);
            clearBtn.onClick.AddListener(Clear);
            flipXBtn.onClick.AddListener(FlipX);
            pointMenu.gameObject.SetActive(true);
            pointMenu.gameObject.SetActive(false);
            pointItemList = gameObject.GetComponent<PointItemList>();
            pointItemList.Init(DisplayPointInfo);
            pointItemList.screen = screen;
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
            pointMenu.SetTitle("Point " + pointItemList.GetItemIndex(pointData));
        }
        private void RemovePoint()
        {
            bool isRemoved = pointItemList.RemovePoint();
            if (isRemoved) DisplayPointInfo(null);
        }
        private void Clear()
        {
            pointItemList.RemoveDataList();
            DisplayPointInfo(null);
        }
        private void FlipX() => pointItemList.FlipX();
        private void SelectPointType(int type) => pointType = type;
        private void EnableAddPoint(bool isEnabled) => isEnabledAddPoint = isEnabled;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isEnabledAddPoint && objectDataObserver != null && !isDragging)
                CreateNewPoint(screen.GetActualPosition(eventData.pointerCurrentRaycast.worldPosition));
        }
        private void CreateNewPoint(Vector2 pos)
        {
            PointDataObserver pointData = new();
            pointData.ChangePosition(pos);
            pointItemList.SetTypeToLastPoint(pointType == 0);
            pointItemList.CreateItemAndAddData(pointData);
        }
        protected override void CreateObject(ObjectDataObserver data) { }
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data == objectDataObserver) return;
            pointItemList.Clear();
            DisplayPointInfo(null);
            objectDataObserver = data;
            if (data != null)
                pointItemList.DisplayDataList(data.moveData);
        }
        protected override void RemoveObject(ObjectDataObserver data) => SelectObject(null);
        protected override void SelectWave(WaveDataObserver data) => SelectObject(null);
    }
}