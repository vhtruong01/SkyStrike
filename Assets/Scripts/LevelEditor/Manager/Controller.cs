using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
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

            public void Awake()
            {
                metaDataDict = new();
                foreach (var item in metaDataList)
                    metaDataDict.Add(item.id, item);
                EventManager.onGetMetaData.AddListener(GetMetaData);
            }
            private EnemyMetaData GetMetaData(int id) => metaDataDict.GetValueOrDefault(id);
            public void OnDisable() => _mainCam = null;

            public void Start()
            {
                var allObject = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                foreach (var obj in allObject)
                {
                    if (obj is not Menu) continue;
                    var menu = obj as Menu;
                    bool isActive = menu.gameObject.activeSelf;
                    if (!isActive)
                        menu.gameObject.SetActive(true);
                    menu.Init();
                    if (!isActive)
                        menu.gameObject.SetActive(false);
                }
                LevelDataObserver levelDataObserver = new(ReadFromBinaryFile<LevelData>("test.dat"));
                EventManager.SelectLevel(levelDataObserver);
            }
            public static void SaveLevelData(LevelDataObserver levelData)
            {
                WriteToBinaryFile("test.dat", levelData.ExportData());
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
}