using SkyStrike.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent(typeof(LevelItemList))]
    public class LevelMenu : Menu
    {
        [SerializeField] private Button openBtn;
        [SerializeField] private Button saveNameBtn;
        [SerializeField] private TMP_InputField levelName;
        [SerializeField] private TextMeshProUGUI warning;
        private LevelItemList group;
        private LevelDataObserver levelData;
        private LevelDataObserver curLevelData;
        private HashSet<string> levelSet;
        private Dictionary<string, LevelData> levels;

        protected override void Preprocess()
        {
            group = GetComponent<LevelItemList>();
            group.Init(SelectLevel);
            levelName.characterLimit = 50;
            openBtn.onClick.AddListener(OpenLevel);
            saveNameBtn.onClick.AddListener(ChangeLevelName);
            warning.gameObject.SetActive(false);
            levels = IO.LoadAllLevel<LevelData>();
            levelSet = new(levels.Keys);
            EventManager.onSelectLevel.AddListener(lv => curLevelData = lv);
        }
        public void OnDisable()
        {
            group.SelectNone();
            levelName.text = "";
            DisplayWarning(false);
        }
        public void Start()
        {
            foreach (var key in levels.Keys)
            {
                var lv = levels[key];
                lv.fileName = key;
                group.CreateItem(new(lv));
            }
        }
        private void SelectLevel(LevelDataObserver data)
        {
            if (data == null)
            {
                levelData = null;
                levelName.text = "";
                levelName.interactable = false;
            }
            else
            {
                levelData = data;
                levelName.text = data.fileName.data;
                levelName.interactable = true;
            }
            DisplayWarning(false);
        }
        private void OpenLevel()
        {
            if (levelData != null)
            {
                if (curLevelData.IsEmpty())
                    OnlyOpenLevel();
                else ModalMenu.Show("Save current level?", SaveCurrentAndOpenOtherLevel, OnlyOpenLevel);
            }
        }
        private void SaveCurrentAndOpenOtherLevel()
        {
            SaveLevelData();
            OnlyOpenLevel();
        }
        private void OnlyOpenLevel()
        {
            if (levelData != null)
            {
                Hide();
                EventManager.SelectLevel(levelData);
            }
        }
        private void ChangeLevelName()
        {
            if (levelData == null) return;
            bool isValidName = IsValidName(levelName.text);
            DisplayWarning(!isValidName);
            if (isValidName && IO.RenameLevel(levelData.fileName.data, levelName.text))
            {
                levelData.fileName.SetData(levelName.text);
                levelSet.Add(levelName.text);
            }
        }
        private void DisplayWarning(bool isEnable)
        {
            if (warning.gameObject.activeSelf != isEnable)
                warning.gameObject.SetActive(isEnable);
        }
        private bool IsValidName(string name)
            => !(string.IsNullOrEmpty(name) || levelSet.Contains(name));
        private void OnlyCreateNewLevel()
            => EventManager.SelectLevel(new());
        private void SaveCurrentAndCreateNewLevel()
        {
            SaveLevelData();
            OnlyCreateNewLevel();
        }
        public void CreateEmptyLevel()
            => ModalMenu.Show("Save current level?", SaveCurrentAndCreateNewLevel, OnlyCreateNewLevel);
        public void SaveLevelData()
        {
            string fileName = curLevelData.fileName.data;
            if (string.IsNullOrEmpty(fileName))
                fileName = "NewLevel_" + Random.Range(100000, 999999);
            var data = curLevelData.ExportData();
            PlayerPrefs.SetString("testLevel", fileName);
            if (IO.SaveLevel(data, fileName) && !levelSet.Contains(fileName))
            {
                levelSet.Add(fileName);
                data.fileName = fileName;
                group.CreateItem(new(data));
                curLevelData.fileName.SetData(fileName);
            }
        }
    }
}