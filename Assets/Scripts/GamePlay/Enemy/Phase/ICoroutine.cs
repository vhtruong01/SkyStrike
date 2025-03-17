using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public interface ICoroutine
        {
            public GameObject gameObject { get; }
            public Coroutine StartCoroutine(IEnumerator enumerator);
        }
    }
}