using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [RequireComponent(typeof(MovementController))]
        public class PhaseManager : MonoBehaviour
        {
            public IEnumerator aa(PhaseData phaseData)
            {
                for (int i = 0; i < phaseData.actions.Length; i++)
                {

                }
                yield break;
            }
        }
    }
}