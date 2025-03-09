using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Controller : MonoBehaviour
        {
            private Menu[] menus;
            private LevelDataObserver levelDataObserver;

            public void Awake()
            {
                levelDataObserver = new();
                EventManager.onGetLevel.AddListener(GetLevel);
                menus = FindObjectsByType<Menu>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            }
            public void Start()
            {
                foreach (var menu in menus)
                {
                    bool isActive = menu.gameObject.activeSelf;
                    if (!isActive) menu.Expand();
                    menu.Init();
                    if (!isActive) menu.Collapse();
                }
            }
            private LevelDataObserver GetLevel() => levelDataObserver;
            // save,load,new file....
        }
    }
}