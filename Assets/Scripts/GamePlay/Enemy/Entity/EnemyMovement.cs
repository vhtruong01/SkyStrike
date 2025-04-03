using System.Collections;
using UnityEngine;
using static SkyStrike.Game.MoveData;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyMovement : MonoBehaviour
        {
            public void StarMoving(ObjectData data)
                => StartCoroutine(Move(data));
            private IEnumerator Move(ObjectData data)
            {
                yield return new WaitForSeconds(data.delay);
                MoveData moveData = data.moveData;
                float velocity = data.velocity;
                int index = 0;
                while (index < moveData.points.Length)
                {
                    Point point = moveData.points[index];
                    transform.position = point.midPos.ToVector2().SetZ(transform.position.z);
                    //rotate
                    yield return new WaitForSeconds(point.standingTime);
                    if (index < moveData.points.Length - 1)
                    {
                        Point nextPoint = moveData.points[index + 1];
                        float time = point.travelTime;
                        Vector2 startPos = point.midPos.ToVector2();
                        Vector2 pos2 = point.nextPos.ToVector2();
                        Vector2 pos3 = nextPoint.prevPos.ToVector2();
                        Vector2 endPos = nextPoint.midPos.ToVector2();
                        if (nextPoint.isStraightLine)
                        {
                            if (time <= 0 && velocity > 0) time = (startPos - endPos).magnitude / velocity;
                            yield return StartCoroutine(MoveStraight(startPos, endPos, time));
                        }
                        else
                        {
                            if (time <= 0 && velocity > 0)
                                time = ((startPos - pos2).magnitude
                                    + (pos2 - pos3).magnitude
                                    + (endPos - pos3).magnitude
                                    + (startPos - endPos).magnitude)
                                    / velocity / 2;
                            yield return StartCoroutine(MoveCurve(startPos, pos2, pos3, endPos, time));
                        }
                    }
                    index++;
                }
                print("complete");
            }
            private IEnumerator MoveStraight(Vector2 startPos, Vector2 endPos, float time)
            {
                if (time <= 0) yield break;
                float elapsedTime = 0;
                while (elapsedTime < time)
                {
                    transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / time).SetZ(transform.position.z);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }
            private IEnumerator MoveCurve(Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4, float time)
            {
                if (time <= 0) yield break;
                float elapsedTime = 0;
                while (elapsedTime < time)
                {
                    float t1 = elapsedTime / time;
                    float t2 = 1 - t1;
                    transform.position = (t2 * t2 * t2 * pos1
                        + 3 * t2 * t2 * t1 * pos2
                        + 3 * t1 * t1 * t2 * pos3
                        + t1 * t1 * t1 * pos4
                        ).SetZ(transform.position.z);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }
        }
    }
}