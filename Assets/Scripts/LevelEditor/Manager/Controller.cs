using UnityEngine;
using System.IO;
using SkyStrike.Game;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Controller : MonoBehaviour
        {
            [SerializeField] private Button saveBtn;
            private Menu[] menus;
            private LevelDataObserver levelDataObserver;

            public void Awake()
            {
                saveBtn.onClick.AddListener(WriteLevelData);
                levelDataObserver = new(ReadFromBinaryFile<LevelData>("test.dat"));
                menus = FindObjectsByType<Menu>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                EventManager.onPlay.AddListener(TestLevel);
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
                EventManager.SelectLevel(levelDataObserver);
            }
            private void TestLevel()
            {
                //MainGame.LevelData = levelDataObserver.ToGameData() as LevelData;
                WriteLevelData();
                SceneManager.LoadScene(1);
            }
            public void WriteLevelData()
            {
                var lv = levelDataObserver.ExportData();
                WriteToBinaryFile("test.dat", lv);
                var newLv = ReadFromBinaryFile<LevelData>("test.dat");
                for (int i = 0; i < newLv.waves.Length; i++)
                {
                    string rs = "";
                    for (int j = 0; j < lv.waves[i].objectDataArr.Length; j++)
                    {
                        rs += lv.waves[i].objectDataArr[j].id + "|" + lv.waves[i].objectDataArr[j].refId+"  ";
                    }
                    print(rs);
                }
            }
            public static void WriteToBinaryFile<T>(string fileName, T objectToWrite, bool append = false)
            {
                string dataPath = Path.Combine(Application.persistentDataPath, fileName);
                using Stream stream = File.Open(dataPath, append ? FileMode.Append : FileMode.Create);
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
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return default;
                }
            }
            // save,load,new file....
        }
    }
}