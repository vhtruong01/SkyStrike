using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MovementController : MonoBehaviour
        {
            private Rigidbody2D rig;

            public void Start()
            {
                rig = GetComponent<Rigidbody2D>();
            }
            public IEnumerator Begin(MoveData action)
            {
                if (action.delay > 0)
                    yield return new WaitForSeconds(action.delay);
                Vector2 dir = action.dir.ToVector2();
                Vector2 startPos = new(transform.position.x, transform.position.y);
                Vector2 endPos = dir + startPos;
                Vector2 o = (startPos + endPos) / 2;
                float a = dir.magnitude / 2;
                float b = action.radius;
                float velo = GetComponent<IGameObject>().data.velocity;
                float time = 2 * (b != 0 ? Mathf.PI * Mathf.Sqrt((a * a + b * b) / 2) : a) / velo;
                if (b > 0)
                {
                    float process = 0;
                    Quaternion deg = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, startPos - o));
                    while (process <= 1)
                    {
                        float smoothProcess = Mathf.SmoothStep(0, 1, process);
                        Vector2 newPos = new(Mathf.Cos(smoothProcess * Mathf.PI) * a, Mathf.Sin(smoothProcess * Mathf.PI) * b);
                        newPos = deg * newPos;
                        transform.position = new(newPos.x + o.x, newPos.y + o.y, transform.position.z);
                        process += Time.fixedDeltaTime / time;
                        yield return new WaitForFixedUpdate();
                    }
                }
                else
                {
                    //Vector2 velo2 = new(dir.x / time, dir.y / time);
                    //while (time > 0)
                    //{
                    //    time -= Time.fixedDeltaTime;
                    //    transform.Translate(Time.fixedDeltaTime * velo2);
                    //    yield return new WaitForFixedUpdate();
                    //}
                }
            }
        }
    }
}