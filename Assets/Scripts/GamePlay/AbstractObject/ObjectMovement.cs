using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike.Game
{
    public abstract class ObjectMovement : MonoBehaviour, IMoveable, IObject
    {
        public IObject entity { get; set; }
        protected IEntityMoveData entityMoveData;

        public IEnumerator Travel(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (entityMoveData.pointIndex < entityMoveData.moveData.points.Length - 1)
            {
                Move();
                Point point = entityMoveData.moveData.points[entityMoveData.pointIndex];
                Point nextPoint = entityMoveData.moveData.points[entityMoveData.pointIndex + 1];
                yield return StartCoroutine(
                point.isStraightLine
                    ? MoveStraight(point, nextPoint, entityMoveData.moveData.velocity)
                    : MoveCurve(point, nextPoint, entityMoveData.moveData.velocity));
            }
        }
        protected IEnumerator MoveStraight(Point startPoint, Point nextPoint, float velocity)
        {
            Vector2 startPos = startPoint.midPos.ToVector2();
            Vector2 nextPos = nextPoint.midPos.ToVector2();
            float time = startPoint.isIgnoreVelocity ? startPoint.travelTime : (startPos - nextPos).magnitude / velocity;
            if (time > 0)
            {
                if (!entityMoveData.canMove) yield return null;
                float elapsedTime = 0;
                Rotate(nextPos - startPos);
                while (elapsedTime < time)
                {
                    entity.position = Vector2.Lerp(startPos, nextPos, elapsedTime / time).SetZ(entity.position.z);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }
        }
        protected IEnumerator MoveCurve(Point startPoint, Point nextPoint, float velocity)
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
                if (!entityMoveData.canMove) yield return null;
                float elapsedTime = 0;
                float deltaTime = Time.deltaTime;
                while (elapsedTime < time)
                {
                    elapsedTime += deltaTime;
                    float t1 = elapsedTime / time;
                    float t2 = 1 - t1;
                    Vector3 newPos = (t2 * t2 * t2 * startPos
                        + 3 * t2 * t2 * t1 * pos2
                        + 3 * t1 * t1 * t2 * pos3
                        + t1 * t1 * t1 * nextPos
                        ).SetZ(entity.position.z);
                    var dir = newPos - entity.position;
                    Rotate(dir.normalized);
                    entity.position += dir;
                    deltaTime = Time.deltaTime;
                    yield return null;
                }
            }
        }
        protected void Rotate(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            entity.transform.eulerAngles = new(0, 0, Vector2.SignedAngle(Vector2.up, dir));
        }
        public abstract void Move();
        public abstract void Stop();
    }
}