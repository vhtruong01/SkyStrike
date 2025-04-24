using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static SkyStrike.Game.MoveData;

namespace SkyStrike.Game
{
    public class EnemyMovement : MonoBehaviour, IEnemyComponent, IMoveable
    {
        public EnemyData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Awake()
            => data = GetComponent<EnemyData>();
        public void Move()
        {
            MoveData moveData = data.moveData;
            Point point = moveData.points[data.pointIndex];
            if (data.pointIndex < moveData.points.Length - 1)
            {
                Point nextPoint = moveData.points[data.pointIndex + 1];
                StartCoroutine(
                point.isStraightLine
                    ? MoveStraight(point, nextPoint, moveData.velocity)
                    : MoveCurve(point, nextPoint, moveData.velocity));
            }
            else FinishMoving();
        }
        private IEnumerator MoveStraight(Point startPoint, Point nextPoint, float velocity)
        {
            Vector2 startPos = startPoint.midPos.ToVector2();
            Vector2 nextPos = nextPoint.midPos.ToVector2();
            float time = startPoint.isIgnoreVelocity ? startPoint.travelTime : (startPos - nextPos).magnitude / velocity;
            if (time > 0)
            {
                float elapsedTime = 0;
                Rotate(nextPos - startPos);
                while (elapsedTime < time)
                {
                    transform.position = Vector2.Lerp(startPos, nextPos, elapsedTime / time).SetZ(transform.position.z);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }
            FinishMoving();
        }
        private IEnumerator MoveCurve(Point startPoint, Point nextPoint, float velocity)
        {
            Vector2 startPos = startPoint.midPos.ToVector2();
            Vector2 pos2 = startPoint.nextPos.ToVector2();
            Vector2 pos3 = nextPoint.prevPos.ToVector2();
            Vector2 nextPos = nextPoint.midPos.ToVector2();
            float time = startPoint.isIgnoreVelocity ? startPoint.travelTime
                    : (((startPos - pos2).magnitude
                    + (pos2 - pos3).magnitude
                    + (nextPos - pos3).magnitude
                    + (startPos - nextPos).magnitude)
                    / velocity / 2);
            if (time > 0)
            {
                float elapsedTime = 0;
                float deltaTime = Time.deltaTime;
                float coefficient = 1;
                //
                while (elapsedTime < time)
                {
                    elapsedTime += deltaTime * coefficient;
                    float t1 = elapsedTime / time;
                    float t2 = 1 - t1;
                    Vector3 newPos = (t2 * t2 * t2 * startPos
                        + 3 * t2 * t2 * t1 * pos2
                        + 3 * t1 * t1 * t2 * pos3
                        + t1 * t1 * t1 * nextPos
                        ).SetZ(transform.position.z);
                    //
                    var dir = newPos - transform.position;
                    float len = dir.magnitude;
                    float time2 = len / velocity;
                    coefficient = deltaTime / time2;
                    dir *= coefficient;
                    Rotate(dir.normalized);
                    transform.position += dir;
                    deltaTime = Time.deltaTime;
                    yield return null;
                }
            }
            FinishMoving();
        }
        private void Rotate(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            transform.eulerAngles = new(0, 0, Vector2.SignedAngle(Vector2.up, dir));
        }
        private void FinishMoving()
            => notifyAction?.Invoke(EEntityAction.Arrive);
        public void Interrupt() => StopAllCoroutines();
    }
}