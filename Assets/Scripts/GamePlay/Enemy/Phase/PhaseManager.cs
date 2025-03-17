using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class PhaseManager : MonoBehaviour, ICoroutine
        {
            private List<IPhase> phases;

            public void Awake()
            {
                phases = new List<IPhase>();
                phases.Add(new Phase());
            }
            public void Start()
            {
                foreach (IPhase phase in phases)
                    phase.SetCoroutine(this);
                StartCoroutine(Raid());
            }
            public IEnumerator Raid()
            {
                foreach (IPhase phase in phases)
                    yield return StartCoroutine(phase.StartAction());
            }
        }
    }
}