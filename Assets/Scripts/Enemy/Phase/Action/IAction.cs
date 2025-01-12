using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public interface IAction
        {
            public IAction nextAction { get; set; }
            public GameObject parent {  get; set; }
            public IEnumerator Invoke();
        }
    }
}