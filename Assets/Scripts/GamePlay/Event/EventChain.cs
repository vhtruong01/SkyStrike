using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace SkyStrike.Game
{
    [Serializable]
    public class EventChain
    {
        [field: SerializeField] public EEventSysType eventType { get; private set; }
        [SerializeField] private List<EventLink> links;

        public IEnumerator Active()
        {
            Debug.Log($"-----Event: {eventType}-----");
            for (int i = 0; i < links.Count; i++)
            {
                Debug.Log($"{eventType} - index: {i}, {links[i]}");
                yield return links[i].Active();
            }
            Debug.Log($"-----Done {eventType}-----");
        }

        [Serializable]
        public class EventLink
        {
            [SerializeField] private float duration;
            [SerializeField] private string comment;
            [SerializeField] private UnityEvent<float> events;

            public IEnumerator Active()
            {
                events?.Invoke(duration);
                yield return new WaitForSeconds(duration);
            }
            public override string ToString() => $"duration: {duration}, msg: {comment}";
        }
    }
}