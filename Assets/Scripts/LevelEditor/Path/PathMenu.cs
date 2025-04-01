using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class PathMenu : Menu, IPointerClickHandler, IElementContainer<PointDataObserver>
        {
            [SerializeField] private Button straightPointBtn;
            [SerializeField] private Button curvePointBtn;
            [SerializeField] private NormalButton addPointBtn;
            [SerializeField] private NormalButton snapBtn;
            [SerializeField] private Button removeBtn;
            [SerializeField] private UIGroup switchPointTypeBtn;
            [SerializeField] private PointMenu pointMenu;
            private bool isEnableAddPoint;
            private int pointType;
            private PointItemList pointItemList;
            private ObjectDataObserver objectDataObserver;

            public void Start()
            {
                snapBtn.AddListener(SnappableElement.EnableSnapping, SnappableElement.IsSnap);
                addPointBtn.AddListener(EnableAddPoint, () => isEnableAddPoint);
                removeBtn.onClick.AddListener(RemovePoint);
            }
            public override void Init()
            {
                pointItemList = gameObject.GetComponent<PointItemList>();
                pointItemList.Init(DisplayPointInfo);
                for (int i = 0; i < switchPointTypeBtn.Count; i++)
                    switchPointTypeBtn.GetBaseItem(i).onSelectUI.AddListener(SelectPointType);
                switchPointTypeBtn.SelectFirstItem();
            }
            private void DisplayPointInfo(PointDataObserver pointData)
            {
                pointMenu.Display(pointData);
                pointMenu.Show();
            }
            private void RemovePoint() => pointItemList.RemoveSelectedItem();
            public void SelectPointType(int type) => pointType = type;
            public void EnableAddPoint(bool isEnable) => isEnableAddPoint = isEnable;
            public void OnPointerClick(PointerEventData eventData)
            {
                if (isEnableAddPoint && objectDataObserver != null)
                    CreateNewPoint(eventData.pointerCurrentRaycast.worldPosition);
            }
            private void CreateNewPoint(Vector2 pos)
            {
                PointDataObserver pointData = new();
                pointData.isTraightLine.SetData(pointType == 0);
                pointData.Translate(pos);
                pointItemList.CreateItemAndAddData(pointData);
            }
            protected override void CreateObject(ObjectDataObserver data) { }
            protected override void SelectObject(ObjectDataObserver data)
            {
                pointItemList.Clear();
                DisplayPointInfo(null);
                objectDataObserver = data;
            }
            protected override void RemoveObject(ObjectDataObserver data) => SelectObject(null);
            protected override void SelectWave(WaveDataObserver data) => SelectObject(null);
            public IDataList<PointDataObserver> GetDataList() => objectDataObserver?.moveData;
            public void OnEnable()
            {
                if (objectDataObserver == null || objectDataObserver.refData != null) return;
                pointItemList.DisplayDataList();
            }
        }
    }
}