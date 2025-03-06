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
                MenuManager.onGetLevel.AddListener(GetLevel);
            }
            private LevelDataObserver GetLevel() => levelDataObserver;
            // save,load,new file....
        }
    }
}