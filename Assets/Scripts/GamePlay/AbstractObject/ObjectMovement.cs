using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike.Game
{
    public abstract class ObjectMovement : MonoBehaviour, IMoveable, IObject
    {
        public IObject entity { get; set; }
        protected IEntityMoveData entityMoveData;
        protected Vector3 size = Vector3.one;
        protected abstract float scale { get; set; }

        public IEnumerator Travel(float delay)
        {
            size = Vector3.one * scale;
            Point point = entityMoveData.moveData.points[entityMoveData.pointIndex];
            Point nextPoint = null;
            if (entityMoveData.pointIndex + 1 < entityMoveData.moveData.points.Length)
                nextPoint = entityMoveData.moveData.points[entityMoveData.pointIndex + 1];
            if (point.isScaleImmediately)
                entity.transform.localScale = point.scale * size;
            if (delay > 0)
            {
                Stand();
                yield return new WaitForSeconds(delay);
            }
            if (nextPoint != null)
            {
                Move();
                yield return StartCoroutine(
                point.isStraightLine
                    ? MoveStraight(point, nextPoint, entityMoveData.moveData.velocity)
                    : MoveCurve(point, nextPoint, entityMoveData.moveData.velocity));
            }
            else Stop();
            if (nextPoint != null)
            {
                entity.transform.localScale = nextPoint.scale * size;
                entity.position = nextPoint.midPos.SetZ(entity.position.z);
            }
        }
        protected IEnumerator MoveStraight(Point startPoint, Point nextPoint, float speed)
        {
            Vector2 startPos = startPoint.midPos.ToVector2();
            Vector2 nextPos = nextPoint.midPos.ToVector2();
            float time = startPoint.isIgnoreVelocity ? startPoint.travelTime : (startPos - nextPos).magnitude / speed;
            if (time > 0)
            {
                float elapsedTime = 0;
                Rotate(nextPos - startPos);
                while (elapsedTime < time)
                {
                    elapsedTime += Time.fixedDeltaTime;
                    float delta = elapsedTime / time;
                    if (!startPoint.isScaleImmediately)
                        entity.transform.localScale = size * Lerp(startPoint.scale, nextPoint.scale, delta);
                    entity.position = Vector2.Lerp(startPos, nextPos, delta).SetZ(entity.position.z);
                    yield return new WaitForFixedUpdate();
                }
            }
        }
        private IEnumerator MoveCurve(Point startPoint, Point nextPoint, float speed)
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
                    / 2 / speed);
            if (time > 0)
            {
                float elapsedTime = 0;
                Vector3 lastPost = startPos;
                while (elapsedTime < time)
                {
                    float deltaTime = Time.fixedDeltaTime;
                    float sqrLen = Mathf.Pow(speed * deltaTime, 2);
                    float tempTime = 0;
                    Vector3 dir;
                    do
                    {
                        tempTime += deltaTime;
                        float t1 = (elapsedTime + tempTime) / time;
                        float t2 = 1 - t1;
                        Vector3 newPos = t2 * t2 * t2 * startPos
                            + 3 * t2 * t2 * t1 * pos2
                            + 3 * t1 * t1 * t2 * pos3
                            + t1 * t1 * t1 * nextPos;
                        dir = newPos - lastPost;
                    } while (dir.sqrMagnitude < sqrLen);
                    float coef = deltaTime * speed / dir.magnitude;
                    dir *= coef;
                    elapsedTime += tempTime * coef;
                    Rotate(dir.normalized);
                    lastPost += dir;
                    Rotate(dir.normalized);
                    entity.position += dir;
                    if (!startPoint.isScaleImmediately)
                        entity.transform.localScale = size * Lerp(startPoint.scale, nextPoint.scale, elapsedTime / time);
                    yield return new WaitForFixedUpdate();
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
        public virtual void Stand() { }
        private float Lerp(float a, float b, float t)
            => a + (b - a) * t;
    }
}