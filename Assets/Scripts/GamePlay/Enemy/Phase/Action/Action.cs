using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public abstract class Action : IAction
        {
            protected float delay;
            protected float time;
            public IAction nextAction { get; set; }
            public GameObject parent {  get; set; }

            public abstract IEnumerator Invoke();
        }
    }
}