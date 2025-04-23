using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class PathMenu : ScalableMenu, IPointerClickHandler, IElementContainer<PointDataObserver>
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
        public override void Awake()
        {
            base.Awake();
            addPointBtn.AddListener(EnableAddPoint, () => isEnabledAddPoint);
            removeBtn.onClick.AddListener(RemovePoint);
            clearBtn.onClick.AddListener(Clear);
            flipXBtn.onClick.AddListener(FlipX);
        }
        public override void Init()
        {
            base.Init();
            pointMenu.gameObject.SetActive(true);
            pointMenu.gameObject.SetActive(false);
            pointItemList = gameObject.GetComponent<PointItemList>();
            pointItemList.Init(DisplayPointInfo);
            pointItemList.screen = screen;
            for (int i = 0; i < switchPointTypeBtn.Count; i++)
                switchPointTypeBtn.GetBaseItem(i).onSelectUI.AddListener(SelectPointType);
            switchPointTypeBtn.SelectFirstItem();
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
            bool isRemoved = pointItemList.RemoveSelectedItem();
            if (isRemoved) DisplayPointInfo(null);
        }
        private void Clear()
        {
            pointItemList.Clear();
            DisplayPointInfo(null);
            GetDataList().GetList(out var dataList);
            var pos = dataList[0];
            dataList.Clear();
            pointItemList.CreateItemAndAddData(pos);
        }
        public void FlipX()
        {
            (GetDataList() as MoveDataObserver)?.FlipX();
            pointItemList.DisplayDataList();
        }
        public void SelectPointType(int type) => pointType = type;
        public void EnableAddPoint(bool isEnabled) => isEnabledAddPoint = isEnabled;
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
            if (data == null) return;
            pointItemList.DisplayDataList();
        }
        protected override void RemoveObject(ObjectDataObserver data) => SelectObject(null);
        protected override void SelectWave(WaveDataObserver data) => SelectObject(null);
        public IDataList<PointDataObserver> GetDataList() => objectDataObserver?.moveData;
    }
}