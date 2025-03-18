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
            private MovementController movementController;

            public void Awake()
            {
                movementController = GetComponent<MovementController>();
            }
            public IEnumerator Test(PhaseData phaseData)
            {
                for (int i = 0; i < phaseData.actions.Length; i++)
                {
                    //List<Coroutine> coroutines = new();
                    yield return StartCoroutine(movementController.Begin(phaseData.actions[i].moveData));
                }
            }
        }
    }
}