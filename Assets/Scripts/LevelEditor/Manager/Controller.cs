using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Editor
{
    public class Controller : MonoBehaviour
    {
        private List<IMenu> menus;
        private static LevelDataObserver levelData;

        public void Awake()
        {
            menus = new();
            foreach (var obj in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                (obj as IInitalizable)?.Init();
                if (obj is IMenu)
                    menus.Add(obj as IMenu);
            }
            EventManager.onSelectLevel.AddListener(SelectLevel);
        }
        public void Start()
            => EventManager.SelectLevel(levelData ?? new());
        private void SelectLevel(LevelDataObserver data)
            => levelData = data;
        public void OnDisable()
            => EventManager.Reset();
    }
}