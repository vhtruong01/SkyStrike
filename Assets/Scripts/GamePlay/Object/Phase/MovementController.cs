using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MovementController : MonoBehaviour
        {
            private Rigidbody2D rig;
            public Vec2 velocity { get; set; }

            public void Awake()
            {
                rig = GetComponent<Rigidbody2D>();
            }
            public IEnumerator Begin(MoveData action)
            {
                if (action.delay > 0)
                    yield return new WaitForSeconds(action.delay);
                //rig.linearVelocity=action.
                float time = ((action.dir - new Vector2(transform.position.x, transform.position.y)) / velocity).sqrMagnitude;
                while (time > 0)
                {
                    time -= Time.deltaTime;
                    transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}