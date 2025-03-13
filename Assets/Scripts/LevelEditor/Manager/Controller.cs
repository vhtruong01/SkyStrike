using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Controller : MonoBehaviour
        {
            [SerializeField] private List<Menu> menus;
            private LevelDataObserver levelDataObserver;

            public void Awake()
            {
                levelDataObserver = new();
                EventManager.onGetLevel.AddListener(GetLevel);
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