using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class PathMenu : ScalableMenu, IPointerClickHandler, IElementContainer<PointDataObserver>
        {
            [SerializeField] private Button straightPointBtn;
            [SerializeField] private Button curvePointBtn;
            [SerializeField] private NormalButton addPointBtn;
            [SerializeField] private Button removeBtn;
            [SerializeField] private Button clearBtn;
            [SerializeField] private UIGroup switchPointTypeBtn;
            [SerializeField] private PointMenu pointMenu;
            // flip x, flip y
            private bool isEnableAddPoint;
            private int pointType;
            private PointItemList pointItemList;
            private ObjectDataObserver objectDataObserver;

            public void Start()
            {
                snapBtn.AddListener(screen.EnableSnap, screen.IsSnap);
                addPointBtn.AddListener(EnableAddPoint, () => isEnableAddPoint);
                removeBtn.onClick.AddListener(RemovePoint);
                clearBtn.onClick.AddListener(Clear);
            }
            public override void Init()
            {
                base.Init();
                pointItemList = gameObject.GetComponent<PointItemList>();
                pointItemList.Init(DisplayPointInfo);
                pointItemList.screen = screen;
                for (int i = 0; i < switchPointTypeBtn.Count; i++)
                    switchPointTypeBtn.GetBaseItem(i).onSelectUI.AddListener(SelectPointType);
                switchPointTypeBtn.SelectFirstItem();
            }
            private void DisplayPointInfo(PointDataObserver pointData)
            {
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
                var list = GetDataList().GetList();
                var pos = list[0];
                list.Clear();
                pointItemList.CreateItemAndAddData(pos);
            }
            public void SelectPointType(int type) => pointType = type;
            public void EnableAddPoint(bool isEnable) => isEnableAddPoint = isEnable;
            public void OnPointerClick(PointerEventData eventData)
            {
                if (isEnableAddPoint && objectDataObserver != null && !isDrag)
                    CreateNewPoint(screen.GetActualPosition(eventData.pointerCurrentRaycast.worldPosition));
            }
            private void CreateNewPoint(Vector2 pos)
            {
                PointDataObserver pointData = new();
                pointData.isStraightLine.SetData(pointType == 0);
                pointData.ChangePosition(pos);
                pointItemList.CreateItemAndAddData(pointData);
            }
            protected override void CreateObject(ObjectDataObserver data) { }
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (data == objectDataObserver) return;
                pointItemList.Clear();
                DisplayPointInfo(null);
                objectDataObserver = data;
                if (data == null || data.refData != null) return;
                pointItemList.DisplayDataList();
            }
            protected override void RemoveObject(ObjectDataObserver data) => SelectObject(null);
            protected override void SelectWave(WaveDataObserver data) => SelectObject(null);
            public IDataList<PointDataObserver> GetDataList() => objectDataObserver?.moveData;
        }
    }
}