using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class CurvePoint : UIElement<PointDataObserver>, IPointerClickHandler, IDragHandler
        {
            [SerializeField] private ExtraPoint prevPointIcon;
            [SerializeField] private ExtraPoint nextPointIcon;
            [SerializeField] private RectTransform connectionLine;
            private CurvePoint prev;
            private CurvePoint next;
            public UnityEvent<Vector2> onDrag { get; private set; }

            public void Awake()
            {
                onDrag = new();
                prevPointIcon.call = MovePrevExtraPoint;
                nextPointIcon.call = MoveNextExtraPoint;
            }
            public override void SetData(PointDataObserver data)
            {
                base.SetData(data);
                transform.position = data.midPos.data.SetZ(transform.position.z);
                prevPointIcon.transform.position = data.midPos.data + data.prePos.data;
                MovePrevExtraPoint();
            }
            public void MoveExtraPoint(ExtraPoint curPoint, ExtraPoint otherPoint)
            {
                Vector2 curPos = curPoint.transform.position;
                Vector2 otherPos = 2 * transform.position.ToVector2() - curPos;
                otherPoint.transform.position = new(otherPos.x, otherPos.y, otherPoint.transform.position.z);
                Vector2 dir = curPos - otherPos;
                var angle = -Vector2.SignedAngle(dir, Vector2.right);
                float len = (Controller.mainCam.WorldToScreenPoint(dir) - new Vector3(Screen.width / 2, Screen.height / 2, 0)).magnitude;
                connectionLine.sizeDelta = new(len, connectionLine.sizeDelta.y);
                connectionLine.SetPositionAndRotation(
                    new((curPos.x + otherPos.x) / 2, (curPos.y + otherPos.y) / 2, connectionLine.position.z),
                    Quaternion.Euler(new(connectionLine.eulerAngles.x, connectionLine.eulerAngles.y, angle)));
            }
            public void MoveNextExtraPoint() => MoveExtraPoint(nextPointIcon, prevPointIcon);
            public void MovePrevExtraPoint() => MoveExtraPoint(prevPointIcon, nextPointIcon);
            public override void UnbindData()
            {
            }
            public override void BindData()
            {
            }
        }
    }
}