using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public interface IGameObject
        {
            public GameObject gameObject { get; }
            public void SetData(IGameData data);
            public IEnumerator Appear();
        }
    }
}