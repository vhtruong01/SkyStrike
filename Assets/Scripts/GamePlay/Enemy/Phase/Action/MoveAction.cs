using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class MoveAction : Action
        {
            private int loop;
            private float velocity;
            private List<Vector3> verticies;

            public MoveAction()
            {
                loop = 1;
                velocity = 5;
                verticies = new()
                {
                    new Vector3(-2, -2, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(5, 0, 0),
                    new Vector3 (0, -5, 0),
                };
            }
            public override IEnumerator Invoke()
            {
                parent.transform.position = verticies[0];
                if (delay > 0)
                    yield return new WaitForSeconds(delay);
                while (loop > 0)
                {
                    for (int i = 1; i < verticies.Count; i++)
                    {
                        Vector3 dir = Vector3.Normalize(verticies[i] - verticies[i - 1]);
                        while (dir == Vector3.Normalize(verticies[i] - parent.transform.position))
                        {
                            if (velocity != 0)
                                parent.transform.Translate(Time.deltaTime * velocity * dir);
                            yield return null;
                        }
                        parent.transform.position = verticies[i];
                    }
                    loop--;
                }
                Debug.Log("stop");
            }
        }
    }
}