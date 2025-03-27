using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class PathMenu : Menu, IPointerClickHandler, IElementContainer<PointDataObserver>
        {
            [SerializeField] private Button preActionBtn;
            [SerializeField] private Button nextActionBtn;
            [SerializeField] private Button straightPointBtn;
            [SerializeField] private Button curvePointBtn;
            [SerializeField] private NormalButton addPointBtn;
            [SerializeField] private UIGroup switchPointTypeBtn;
            private bool isEnableAddPoint;
            private int pointType;
            private PointItemList pointItemList;
            private ObjectDataObserver objectDataObserver;
            private MoveDataObserver moveDataObserver;

            public void Start()
            {
                addPointBtn.AddListener(EnableAddPoint, isEnableAddPoint);
            }
            public override void Init()
            {
                moveDataObserver = new MoveDataObserver();
                pointItemList = gameObject.GetComponent<PointItemList>();
                //
                pointItemList.Init(null);
                for (int i = 0; i < switchPointTypeBtn.Count; i++)
                    switchPointTypeBtn.GetBaseItem(i).onSelectUI.AddListener(SelectPointType);
                switchPointTypeBtn.SelectFirstItem();
            }
            public void SelectPointType(int type) => pointType = type;
            public void EnableAddPoint(bool isEnable) => isEnableAddPoint = isEnable;
            //
            public void OnPointerClick(PointerEventData eventData)
            {
                if (isEnableAddPoint)
                {
                    PointDataObserver pointData = new();
                    pointData.isTraight.SetData(pointType == 0);
                    pointData.midPos.SetData(eventData.pointerCurrentRaycast.worldPosition);
                    pointItemList.CreateItem(pointData);
                }
            }
            protected override void CreateObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data)
            {
                objectDataObserver = data;

            }
            protected override void SelectObject(ObjectDataObserver data)
            {
                objectDataObserver = data;
            }

            protected override void SelectWave(WaveDataObserver data)
            {
            }
            private void DrawPath()
            {

            }
            public IDataList<PointDataObserver> GetDataList() => moveDataObserver;
        }
    }
}