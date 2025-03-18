using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MovementController : MonoBehaviour
        {
            private Rigidbody2D rig;
            public void Awake()
            {
                rig = GetComponent<Rigidbody2D>();
            }
            public IEnumerator Begin(MoveData action)
            {
                if (action.delay > 0)
                    yield return new WaitForSeconds(action.delay);
                //rig.linearVelocity=action.
                float delta = 0;
                while (delta < 10)
                {
                    delta += Time.deltaTime;
                    transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}