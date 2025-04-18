using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyMovement : MonoBehaviour
        {
            private Enemy enemy;
            private EnemyBulletSpawner spawner;
            private Vector2 dir;

            public void Awake()
            {
                enemy = GetComponent<Enemy>();
                spawner = GetComponent<EnemyBulletSpawner>();
            }
            public IEnumerator Move()
            {
                MoveData moveData = enemy.data.moveData;
                dir = Vector2.down;
                transform.eulerAngles = new(0, 0, 0);
                int index = 0;
                while (index < moveData.points.Length)
                {
                    Point point = moveData.points[index];
                    EnemyBulletData bulletData = point.bulletDataList.Length > 0 ? point.bulletDataList[0] : null;
                    spawner.SetData(bulletData);
                    yield return new WaitForSeconds(point.standingTime);
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
                if (!enemy.data.isMaintain)
                    enemy.Disappear();
            }
            private IEnumerator MoveStraight(Point startPoint, Point nextPoint, float velocity)
            {
                Vector2 startPos = startPoint.midPos.ToVector2();
                Vector2 nextPos = nextPoint.midPos.ToVector2();
                float time = (startPos - nextPos).magnitude / velocity;
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
                float time = ((startPos - pos2).magnitude
                        + (pos2 - pos3).magnitude
                        + (nextPos - pos3).magnitude
                        + (startPos - nextPos).magnitude)
                        / velocity / 2;
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
        }
    }
}