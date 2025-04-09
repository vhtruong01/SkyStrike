using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyMovement : MonoBehaviour
        {
            private MoveData moveData;
            private Vector2 dir;

            public IEnumerator Move(MoveData moveData)
            {
                dir = Vector2.down;
                transform.eulerAngles = new();
                this.moveData = moveData;
                if (moveData.velocity > 0)
                {
                    int index = 0;
                    while (index < moveData.points.Length)
                    {
                        Point point = moveData.points[index];
                        transform.position = point.midPos.ToVector2().SetZ(transform.position.z);
                        yield return new WaitForSeconds(point.standingTime);
                        //rotate
                        if (index < moveData.points.Length - 1)
                        {
                            Point nextPoint = moveData.points[index + 1];
                            yield return StartCoroutine(
                                nextPoint.isStraightLine
                                ? MoveStraight(point, nextPoint)
                                : MoveCurve(point, nextPoint));
                        }
                        index++;
                    }
                }
                GetComponent<Enemy>().Release();
            }
            private IEnumerator MoveStraight(Point startPoint, Point nextPoint)
            {
                Vector2 startPos = startPoint.midPos.ToVector2();
                Vector2 nextPos = nextPoint.midPos.ToVector2();
                float time = (startPos - nextPos).magnitude / moveData.velocity;
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
            private IEnumerator MoveCurve(Point startPoint, Point nextPoint)
            {
                Vector2 startPos = startPoint.midPos.ToVector2();
                Vector2 pos2 = startPoint.nextPos.ToVector2();
                Vector2 pos3 = nextPoint.prevPos.ToVector2();
                Vector2 nextPos = nextPoint.midPos.ToVector2();
                float time = ((startPos - pos2).magnitude
                        + (pos2 - pos3).magnitude
                        + (nextPos - pos3).magnitude
                        + (startPos - nextPos).magnitude)
                        / moveData.velocity / 2;
                if (time <= 0) yield break;
                float elapsedTime = 0;
                //
                while (elapsedTime < time)
                {
                    float t1 = elapsedTime / time;
                    float t2 = 1 - t1;
                    Vector3 newPos = (t2 * t2 * t2 * startPos
                        + 3 * t2 * t2 * t1 * pos2
                        + 3 * t1 * t1 * t2 * pos3
                        + t1 * t1 * t1 * nextPos
                        ).SetZ(transform.position.z);
                    Rotate((newPos - transform.position).normalized);
                    transform.position = newPos;
                    yield return null;
                    elapsedTime += Time.deltaTime;
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