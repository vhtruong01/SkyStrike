using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using SkyStrike.Game;

namespace SkyStrike.Editor
{
    public class Controller : MonoBehaviour
    {
        protected static Camera _mainCam;
        public static Camera mainCam
        {
            get
            {
                if (_mainCam == null)
                    _mainCam = Camera.main;
                return _mainCam;
            }
        }
        [SerializeField] private List<EnemyMetaData> metaDataList;
        private Dictionary<int, EnemyMetaData> metaDataDict;
        private List<IMenu> menus;
        private LevelDataObserver levelDataObserver;

        private EnemyMetaData GetMetaData(int id) => metaDataDict.GetValueOrDefault(id);
        public void OnDisable()
            => _mainCam = null;
        public void Awake()
        {
            //PlayerPrefs.DeleteAll();
            metaDataDict = new();
            menus = new();
            foreach (var item in metaDataList)
                metaDataDict.Add(item.id, item);
            EventManager.onGetMetaData.AddListener(GetMetaData);
            var allObject = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var obj in allObject)
            {
                UIGroup uIGroup = obj as UIGroup;
                if (uIGroup != null)
                    uIGroup.Init();
                if (obj is IMenu)
                    menus.Add(obj as IMenu);
            }
            foreach (var menu in menus)
            {
                menu.Init();
                menu.Restore();
            }
            levelDataObserver = new(ReadFromBinaryFile<LevelData>("test.dat"));
            EventManager.SelectLevel(levelDataObserver);
        }
        private void SaveSetting()
        {
            foreach (var menu in menus)
                menu.SaveSetting();
        }
        public void SaveLevelData()
        {
            WriteToBinaryFile("test.dat", levelDataObserver.ExportData());
            SaveSetting();
            print("saved");
        }
        public static void WriteToBinaryFile<T>(string fileName, T objectToWrite)
        {
            string dataPath = Path.Combine(Application.persistentDataPath, fileName);
            using Stream stream = File.Open(dataPath, FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(stream, objectToWrite);
        }
        public static T ReadFromBinaryFile<T>(string fileName)
        {
            try
            {
                string dataPath = Path.Combine(Application.persistentDataPath, fileName);
                using Stream stream = File.Open(dataPath, FileMode.Open);
                return (T)new BinaryFormatter().Deserialize(stream);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return default;
            }
        }
    }
}