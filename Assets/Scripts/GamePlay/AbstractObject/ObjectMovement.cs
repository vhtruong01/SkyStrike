using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike.Game
{
    public abstract class ObjectMovement : MonoBehaviour, IMoveable, IObject
    {
        public IObject entity { get; set; }
        protected IEntityMoveData entityMoveData;
        protected abstract float scale { get; set; }

        public IEnumerator Travel(float delay)
        {
            Point point = entityMoveData.moveData.points[entityMoveData.pointIndex];
            Point nextPoint = null;
            if (entityMoveData.pointIndex + 1 < entityMoveData.moveData.points.Length)
                nextPoint = entityMoveData.moveData.points[entityMoveData.pointIndex + 1];
            if (point.isScaleImmediately)
                entity.transform.localScale = Vector3.one * point.scale;
            yield return new WaitForSeconds(delay);
            if (nextPoint != null)
            {
                Move();
                yield return StartCoroutine(
                point.isStraightLine
                    ? MoveStraight(point, nextPoint, entityMoveData.moveData.velocity)
                    : MoveCurve(point, nextPoint, entityMoveData.moveData.velocity));
            }
            else Stop();
            entity.transform.localScale = Vector3.one * point.scale;
            scale = point.scale;
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
                    float delta = elapsedTime / time;
                    if (!startPoint.isScaleImmediately)
                        entity.transform.localScale = Vector3.one * Lerp(scale, startPoint.scale, delta);
                    entity.position = Vector2.Lerp(startPos, nextPos, delta).SetZ(entity.position.z);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }

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
                while (elapsedTime < time)
                {
                    elapsedTime += deltaTime * coefficient;
                    float t1 = elapsedTime / time;
                    float t2 = 1 - t1;
                    Vector3 newPos = (t2 * t2 * t2 * startPos
                        + 3 * t2 * t2 * t1 * pos2
                        + 3 * t1 * t1 * t2 * pos3
                        + t1 * t1 * t1 * nextPos
                        ).SetZ(entity.position.z);
                    var dir = newPos - entity.position;
                    float len = dir.magnitude;
                    float time2 = len / velocity;
                    coefficient = deltaTime / time2;
                    dir *= coefficient;
                    Rotate(dir.normalized);
                    entity.position += dir;
                    deltaTime = Time.deltaTime;
                    if (!startPoint.isScaleImmediately)
                        entity.transform.localScale = Vector3.one * Lerp(scale, startPoint.scale, t1);
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
        private float Lerp(float a, float b, float t)
            => a + (b - a) * t;
    }
}