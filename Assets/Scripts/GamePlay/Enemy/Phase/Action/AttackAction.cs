using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class AttackAction : Action
        {
            public override IEnumerator Invoke()
            {
                Debug.Log(2);
                yield return new WaitForSeconds(5);
            }
        }
    }
}