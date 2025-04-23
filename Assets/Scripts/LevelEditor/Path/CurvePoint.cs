using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike.Editor
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
            extraPoint2.Enable(false);
        }
        private void RefreshName()
        {
            itemName.text = index.Value.ToString();
            if (nextPoint != null)
                nextPoint.RefreshName();
        }
        public void SetPrevPoint(CurvePoint point)
        {
            extraPoint1.Enable(!(point == null || point.data.isStraightLine.data));
            prevPoint = point;
            if (prevPoint != null)
                prevPoint.SetNextPoint(this);
            else line.Clear();
            RefreshName();
            if (index.Value % 2 != 0)
                (extraPoint1.image.color, extraPoint2.image.color) = (extraPoint2.image.color, extraPoint1.image.color);
        }
        public void SetNextPoint(CurvePoint point)
        {
            nextPoint = point;
            extraPoint2.Enable(!(point == null || data.isStraightLine.data));
            DrawLine();
        }
        private void Render(ExtraPoint curPoint, ExtraPoint otherPoint)
        {
            Vector2 midPos = transform.position;
            Vector2 curPos = curPoint.transform.position;
            Vector2 otherPos = 2 * midPos - curPos;
            curPoint.Render(curPos, midPos, screen.scale);
            otherPoint.Render(otherPos, midPos, screen.scale);
        }
        private void MoveExtraPoint(ExtraPoint curPoint, ExtraPoint otherPoint)
        {
            Render(curPoint, otherPoint);
            if (!data.isStraightLine.data)
                DrawLine();
            if (prevPoint != null && !prevPoint.data.isStraightLine.data)
                prevPoint.DrawLine();
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
            if (nextPoint != null)
                line.Draw(data.isStraightLine.data ? CreateStraightLine() : CreateCurveLine());
            else line.Clear();
        }
        private Vector2[] CreateStraightLine()
        {
            Vector2 dir = nextPoint.transform.position - transform.position;
            int pointCount = Mathf.CeilToInt((dir.magnitude - space / 2) / space);
            if (pointCount == 0) pointCount = 1;
            dir = dir.normalized;
            Vector2[] positions = new Vector2[pointCount - 1];
            for (int i = 1; i < pointCount; i++)
                positions[i - 1] = i * space * dir + transform.position.ToVector2();
            return positions;
        }
        private Vector2[] CreateCurveLine()
        {
            float len = (transform.position - extraPoint2.transform.position).magnitude
                + (extraPoint2.transform.position - nextPoint.extraPoint1.transform.position).magnitude
                + (nextPoint.extraPoint1.transform.position - nextPoint.transform.position).magnitude
                + (nextPoint.transform.position - transform.position).magnitude;
            int pointCount = Mathf.CeilToInt(len / 2 / space);
            if (pointCount == 0) pointCount = 1;
            Vector2[] positions = new Vector2[pointCount - 1];
            for (int i = 1; i < pointCount; i++)
            {
                float t1 = 1f * i / pointCount;
                float t2 = 1 - t1;
                positions[i - 1] = (t2 * t2 * t2 * transform.position
                    + 3 * t2 * t2 * t1 * extraPoint2.transform.position
                    + 3 * t1 * t1 * t2 * nextPoint.extraPoint1.transform.position
                    + t1 * t1 * t1 * nextPoint.transform.position
                    ).ToVector2();
            }
            return positions;
        }
        protected override void Refresh()
        {
            DrawLine();
            if (prevPoint != null)
                prevPoint.DrawLine();
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            data.ChangePosition(screen.GetActualPosition(transform.position));
            Refresh();
        }
        public void ChangePointType(bool isStraight)
        {
            if (nextPoint == null)
                extraPoint2.Enable(false);
            else
            {
                nextPoint.extraPoint1.Enable(!isStraight);
                extraPoint2.Enable(!isStraight);
                DrawLine();
            }
        }
        public override void UnbindData()
        {
            data.isStraightLine.Unbind(ChangePointType);
            data.midPos.Unbind(SetPosition);
        }
        public override void BindData()
        {
            data.isStraightLine.Bind(ChangePointType);
            data.midPos.Bind(SetPosition);
        }
    }
}