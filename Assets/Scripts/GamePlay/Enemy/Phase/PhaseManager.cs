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
                int i = 1;
                for (i = 0; i < phaseData.moveDataList.Length; i++)
                {
                    yield return StartCoroutine(movementController.Begin(phaseData.moveDataList[i]));
                }
            }
            //private IEnumerator Move(MoveData[] actions)
            //{
            //}
        }
    }
}