using System;
using UnityEngine;
using System.Collections.Generic;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
    public class GameManager : ScriptableObject
    {
        [Serializable]
        public class AllLevelInfo
        {
            public int totalStar;
            public List<int> levels;
            public AllLevelInfo()
            {
                levels = new()
                {
                    0
                };
            }
        }
        [field: SerializeField, ReadOnly] public int curLevelIndex { get; set; }
        public LevelData curLevel => levelDataList[curLevelIndex];
        public int maxLevel { get; private set; }
        public List<LevelData> levelDataList { get; private set; }
        private AllLevelInfo allLevelInfo;

        private void OnEnable()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = 1000;
#elif UNITY_WEBGL            
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
#else
            Application.targetFrameRate = 60;
#endif
            levelDataList = new(IO.LoadAllLevel<LevelData>().Values);
            allLevelInfo = JsonUtility.FromJson<AllLevelInfo>(PlayerPrefs.GetString("levels", "")) ?? new();
            maxLevel = Mathf.Clamp(allLevelInfo.levels.Count - 1, 0, levelDataList.Count - 1);
            curLevelIndex = maxLevel;
        }
        public void SaveCurrentLevel(bool isComplete, int score, int star)
        {
            allLevelInfo.totalStar += star;
            allLevelInfo.levels[curLevelIndex] = Mathf.Max(score, allLevelInfo.levels[curLevelIndex]);
            if (isComplete && levelDataList.Count - 1 > curLevelIndex)
            {
                curLevelIndex++;
                if (allLevelInfo.levels.Count <= curLevelIndex)
                    allLevelInfo.levels.Add(0);
                maxLevel = Mathf.Max(maxLevel,curLevelIndex);
            }
            PlayerPrefs.SetString("levels", JsonUtility.ToJson(allLevelInfo));
        }
        public int GetScore(int index)
        {
            if (index >= allLevelInfo.levels.Count) return 0;
            return allLevelInfo.levels[index];
        }
    }
}