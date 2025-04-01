using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class CurvePoint : UIElement<PointDataObserver>, IPointerClickHandler, IDragHandler
        {
            private static readonly float space = 0.5f;
            [SerializeField] private ExtraPoint extraPoint1;
            [SerializeField] private ExtraPoint extraPoint2;
            [SerializeField] private RectTransform connectionLine1;
            [SerializeField] private RectTransform connectionLine2;
            [SerializeField] private Line line;
            private CurvePoint prevPoint;
            private CurvePoint nextPoint;
            public UnityEvent<Vector2> onDrag { get; private set; }

            public void Awake()
            {
                onDrag = new();
                extraPoint1.call = MovePrevExtraPoint;
                extraPoint2.call = MoveNextExtraPoint;
            }
            public override void RemoveData()
            {
                if (nextPoint != null)
                    nextPoint.SetPrevPoint(prevPoint);
                else if (prevPoint != null)
                    prevPoint.SetNextPoint(nextPoint);
                nextPoint = prevPoint = null;
                base.RemoveData();
            }
            public override void SetData(PointDataObserver data)
            {
                base.SetData(data);
                transform.position = data.midPos.data.SetZ(transform.position.z);
                extraPoint1.transform.position = data.prePos.data.SetZ(extraPoint1.transform.position.z);
                Render(extraPoint1, extraPoint2);
            }
            public void SetPrevPoint(CurvePoint point)
            {
                extraPoint1.Enable(!(point == null || data.isTraightLine.data));
                prevPoint = point;
                prevPoint?.SetNextPoint(this);
                if (GetPointIndex() % 2 != 0)
                    (extraPoint1.image.color, extraPoint2.image.color) = (extraPoint2.image.color, extraPoint1.image.color);
                RefreshPath();
            }
            public void SetNextPoint(CurvePoint point)
            {
                extraPoint2.Enable(!(point == null || point.data.isTraightLine.data));
                nextPoint = point;
            }
            private void Render(ExtraPoint curPoint, ExtraPoint otherPoint)
            {
                Vector2 curPos = curPoint.transform.position;
                Vector2 otherPos = 2 * transform.position.ToVector2() - curPos;
                curPoint.Render(curPos, transform.position);
                otherPoint.Render(otherPos, transform.position);
            }
            private void MoveExtraPoint(ExtraPoint curPoint, ExtraPoint otherPoint)
            {
                Render(curPoint, otherPoint);
                if (!data.isTraightLine.data)
                    DrawLine();
                if (nextPoint != null && !nextPoint.data.isTraightLine.data)
                    nextPoint.DrawLine();
            }
            private void MoveNextExtraPoint(Vector2 pos)
            {
                data.nextPos.SetData(pos);
                MoveExtraPoint(extraPoint2, extraPoint1);
                data.prePos.SetData(extraPoint1.transform.position);
            }
            private void MovePrevExtraPoint(Vector2 pos)
            {
                data.prePos.SetData(pos);
                MoveExtraPoint(extraPoint1, extraPoint2);
                data.nextPos.SetData(extraPoint2.transform.position);
            }
            private void DrawLine()
            {
                if (prevPoint != null)
                    line.Draw(data.isTraightLine.data ? CreateStraightLine(prevPoint) : CreateCurveLine(prevPoint));
            }
            private Vector2[] CreateStraightLine(CurvePoint endPoint)
            {
                Vector2 dir = endPoint.transform.position - transform.position;
                int pointCount = Mathf.CeilToInt(dir.magnitude / space);
                if (pointCount == 0) pointCount = 1;
                dir = dir.normalized;
                Vector2[] positions = new Vector2[pointCount - 1];
                for (int i = 1; i < pointCount; i++)
                    positions[i - 1] = i * space * dir + transform.position.ToVector2();
                return positions;
            }
            private Vector2[] CreateCurveLine(CurvePoint endPoint)
            {
                Vector2 dir = endPoint.transform.position - transform.position;
                int pointCount = Mathf.CeilToInt(dir.magnitude / space * 1.5f);
                if (pointCount == 0) pointCount = 1;
                Vector2[] positions = new Vector2[pointCount - 1];
                for (int i = 1; i < pointCount; i++)
                {
                    float t1 = 1f * i / pointCount;
                    float t2 = 1 - t1;
                    positions[i - 1] = (t2 * t2 * t2 * transform.position
                        + 3 * t2 * t2 * t1 * extraPoint1.transform.position
                        + 3 * t1 * t1 * t2 * endPoint.extraPoint2.transform.position
                        + t1 * t1 * t1 * endPoint.transform.position
                        ).ToVector2();
                }
                return positions;
            }
            private void RefreshPath()
            {
                DrawLine();
                if (nextPoint != null)
                    nextPoint.DrawLine();
            }
            private int GetPointIndex() => 1 + (prevPoint != null ? prevPoint.GetPointIndex() : -1);
            public override void OnDrag(PointerEventData eventData)
            {
                base.OnDrag(eventData);
                data.Translate(transform.position);
                RefreshPath();
            }
            public void SetPosition(Vector2 newPos)
            {
                if (newPos.x == transform.position.x && newPos.y == transform.position.y) return;
                transform.position = newPos.SetZ(transform.position.z);
                RefreshPath();
            }
            public override void UnbindData()
            {
                data.midPos.Unbind(SetPosition);
            }
            public override void BindData()
            {
                data.midPos.Bind(SetPosition);
            }
        }
    }
}