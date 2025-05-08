using System.Collections.Generic;
using UnityEngine;
using SkyStrike.Game;

namespace SkyStrike.Editor
{
    public class Controller : MonoBehaviour
    {
        private List<IMenu> menus;
        private LevelDataObserver levelData;

        public void Awake()
        {
            menus = new();
            foreach (var obj in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                (obj as IInitalizable)?.Init();
                if (obj is IMenu)
                    menus.Add(obj as IMenu);
            }
        }
        public void Start()
        {
            //PlayerPrefs.DeleteAll();
            levelData = new(IO.ReadFromBinaryFile<LevelData>("test.dat"));
            EventManager.SelectLevel(levelData);        
        }
        public void SaveLevelData()
        {
            IO.WriteToBinaryFile("test.dat", levelData.ExportData());
            foreach (var menu in menus)
                menu.SaveSetting();
            print("saved");
        }
    }
}