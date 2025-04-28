using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class EventController : MonoBehaviour
    {
        [SerializeField] private List<EventChain> chains;

        public void Awake()
            => EventManager.SubscribeSysEvent(eventType => StartCoroutine(ActiveEvent(eventType)));
        private IEnumerator ActiveEvent(EEventSysType eventType)
        {
            foreach (var chain in chains)
                if (chain.eventType == eventType)
                    yield return StartCoroutine(chain.Active());
        }
    }
}