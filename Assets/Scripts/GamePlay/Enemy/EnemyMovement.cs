using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike.Game
{
    public class EnemyMovement : EnemyComponent
    {
        private Vector2 dir;
        private MoveData moveData;

        public override void SetData(EnemyData data)
        {
            base.SetData(data);
            moveData = data.moveData;
        }
        public void Move() => StartCoroutine(Go());
        private IEnumerator Go()
        {
            dir = Vector2.up;
            transform.eulerAngles = new(0, 0, 0);
            int index = 0;
            while (index < moveData.points.Length)
            {
                Point point = moveData.points[index];
                EnemyBulletData bulletData = null;
                if (point.bulletDataList != null && point.bulletDataList.Length > 0)
                    bulletData = point.bulletDataList[0];
                data.bulletData = bulletData;
                data.shield = point.shield;
                data.isImmortal = point.isImmortal;
                notifyAction?.Invoke(EEnemyAction.Attack);
                notifyAction?.Invoke(EEnemyAction.Defend);
                if (point.standingTime > 0)
                {
                    notifyAction?.Invoke(EEnemyAction.Stand);
                    yield return new WaitForSeconds(point.standingTime);
                }
                notifyAction?.Invoke(EEnemyAction.Move);
                if (index < moveData.points.Length - 1)
                {
                    Point nextPoint = moveData.points[index + 1];
                    yield return StartCoroutine(
                        point.isStraightLine
                        ? MoveStraight(point, nextPoint, moveData.velocity)
                        : MoveCurve(point, nextPoint, moveData.velocity));
                }
                index++;
            }
            notifyAction?.Invoke(EEnemyAction.Disappear);
        }
        private IEnumerator MoveStraight(Point startPoint, Point nextPoint, float velocity)
        {
            Vector2 startPos = startPoint.midPos.ToVector2();
            Vector2 nextPos = nextPoint.midPos.ToVector2();
            float time = startPoint.isIgnoreVelocity ? startPoint.travelTime : (startPos - nextPos).magnitude / velocity;
            if (time <= 0) yield break;
            float elapsedTime = 0;
            Rotate(nextPos - startPos);
            while (elapsedTime < time)
            {
                transform.position = Vector2.Lerp(startPos, nextPos, elapsedTime / time).SetZ(transform.position.z);
                yield return null;
                elapsedTime += Time.deltaTime;
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
            if (time <= 0) yield break;
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
        private void Rotate(Vector2 newDir)
        {
            if (newDir == Vector2.zero) return;
            transform.Rotate(0, 0, Vector2.SignedAngle(dir, newDir));
            dir = newDir;
        }
        public override void Interrupt() => StopAllCoroutines();
    }
}