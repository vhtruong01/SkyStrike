using System.Collections;
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
            public IEnumerator Begin(PhaseData phaseData)
            {
                yield return StartCoroutine(Move(phaseData.moveDataList));
            }
            private IEnumerator Move(MoveData[] actions)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    print(actions[i]);
                    yield return StartCoroutine(movementController.Begin(actions[i]));
                }
            }
        }
    }
}