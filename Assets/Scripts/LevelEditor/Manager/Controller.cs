using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Controller : MonoBehaviour
        {
            private LevelDataObserver levelDataObserver;

            public void Awake()
            {
                levelDataObserver = new();
            }
            public void Start()
            {
                MenuManager.onCreateWave += levelDataObserver.CreateNewWave;
            }
        }
    }
}