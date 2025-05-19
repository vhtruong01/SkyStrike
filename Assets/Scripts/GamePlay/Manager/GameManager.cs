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
        public List<LevelData> levelDataList { get; private set; }
        private AllLevelInfo allLevelInfo;

        private void OnEnable()
        {
            levelDataList = new(IO.LoadAllLevel<LevelData>().Values);
            allLevelInfo = JsonUtility.FromJson<AllLevelInfo>(PlayerPrefs.GetString("levels", "")) ?? new();
            curLevelIndex = Mathf.Clamp(Mathf.Max(0, allLevelInfo.levels.Count - 1), 0, levelDataList.Count - 1);
        }
        public void SaveCurrentLevel(bool isComplete, int score, int star)
        {
            allLevelInfo.totalStar += star;
            allLevelInfo.levels[curLevelIndex] = Mathf.Max(score, allLevelInfo.levels[curLevelIndex]);
            if (isComplete && allLevelInfo.levels.Count - 1 == curLevelIndex && levelDataList.Count - 1 > curLevelIndex)
            {
                if (allLevelInfo.levels.Count <= curLevelIndex)
                    allLevelInfo.levels.Add(0);
                else allLevelInfo.levels[curLevelIndex] = 0;
                curLevelIndex++;
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