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
                if(action.delay > 0) 
                    yield return new WaitForSeconds(action.delay);
                //rig.linearVelocity=action.
                yield return null;
            }
        }
    }
}