using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class CurvePoint : MoveableUIElement<PointDataObserver>
        {
            private static readonly float space = 0.5f;
            [SerializeField] private ExtraPoint extraPoint1;
            [SerializeField] private ExtraPoint extraPoint2;
            [SerializeField] private Line line;
            private CurvePoint prevPoint;
            private CurvePoint nextPoint;
            public UnityEvent<Vector2> onDrag { get; private set; }

            public override void Init()
            {
                base.Init();
                line.Init();
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
                transform.position = screen.GetPositionOnScreen(data.midPos.data).SetZ(transform.position.z);
                extraPoint1.transform.position = screen.GetPositionOnScreen(data.prePos.data).SetZ(extraPoint1.transform.position.z);
                Render(extraPoint1, extraPoint2);
            }
            public void SetPrevPoint(CurvePoint point)
            {
                extraPoint1.Enable(!(point == null || data.isStraightLine.data));
                prevPoint = point;
                prevPoint?.SetNextPoint(this);
                Rename(index.Value);
                if (index.Value % 2 != 0)
                    (extraPoint1.image.color, extraPoint2.image.color) = (extraPoint2.image.color, extraPoint1.image.color);
                Refresh();
            }
            private void Rename(int pointIndex)
            {
                itemName.text = pointIndex.ToString();
                if (nextPoint != null)
                    nextPoint.Rename(pointIndex + 1);
            }
            public void SetNextPoint(CurvePoint point)
            {
                extraPoint2.Enable(!(point == null || point.data.isStraightLine.data));
                nextPoint = point;
            }
            private void Render(ExtraPoint curPoint, ExtraPoint otherPoint)
            {
                Vector2 midPos = transform.position;
                Vector2 curPos = curPoint.transform.position;
                Vector2 otherPos = 2 * midPos - curPos;
                curPoint.Render(curPos, midPos, screen.GetScale);
                otherPoint.Render(otherPos, midPos, screen.GetScale);
            }
            private void MoveExtraPoint(ExtraPoint curPoint, ExtraPoint otherPoint)
            {
                Render(curPoint, otherPoint);
                if (!data.isStraightLine.data)
                    DrawLine();
                if (nextPoint != null && !nextPoint.data.isStraightLine.data)
                    nextPoint.DrawLine();
            }
            private void MoveNextExtraPoint(Vector2 pos)
            {
                data.nextPos.OnlySetData(screen.GetActualPosition(pos));
                MoveExtraPoint(extraPoint2, extraPoint1);
                data.prePos.OnlySetData(screen.GetActualPosition(extraPoint1.transform.position));
            }
            private void MovePrevExtraPoint(Vector2 pos)
            {
                data.prePos.OnlySetData(screen.GetActualPosition(pos));
                MoveExtraPoint(extraPoint1, extraPoint2);
                data.nextPos.OnlySetData(screen.GetActualPosition(extraPoint2.transform.position));
            }
            private void DrawLine()
            {
                if (prevPoint != null)
                    line.Draw(data.isStraightLine.data ? CreateStraightLine(prevPoint) : CreateCurveLine(prevPoint));
                else line.Clear();
            }
            private Vector2[] CreateStraightLine(CurvePoint endPoint)
            {
                Vector2 dir = endPoint.transform.position - transform.position;
                int pointCount = Mathf.CeilToInt((dir.magnitude - space / 2) / space);
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
            protected override void Refresh()
            {
                DrawLine();
                if (nextPoint != null)
                    nextPoint.DrawLine();
            }
            public override void OnDrag(PointerEventData eventData)
            {
                base.OnDrag(eventData);
                data.ChangePosition(screen.GetActualPosition(transform.position));
                Refresh();
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