using SkyStrike.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LevelMenu : Menu
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelIcon levelIcon0Prefab;
        [SerializeField] private LevelIcon levelIcon1Prefab;
        [SerializeField] private TextMeshProUGUI title;
        private LevelIcon prevLevelIcon;

        public override void Start()
        {
            var scroll = GetComponent<ScrollRect>();
            int index = 0;
            for (int i = 0; i < gameManager.levelDataList.Length; i++)
            {
                var levelData = gameManager.levelDataList[i];
                if (levelData == null) continue;
                var icon = Instantiate(index % 2 == 0 ? levelIcon0Prefab : levelIcon1Prefab, scroll.content.transform, false);
                icon.isLock = i > gameManager.curLevelIndex;
                icon.Init(levelData, index, SelectLevel);
                if (index == gameManager.curLevelIndex)
                    icon.Appear();
                index++;
            }
            var layoutGroup = scroll.content.GetComponentInChildren<HorizontalOrVerticalLayoutGroup>();
            float scrollW = scroll.GetComponent<RectTransform>().rect.width;
            float iconW = levelIcon0Prefab.GetComponent<RectTransform>().rect.width;
            int padding = (int)((scrollW - iconW) / 2);
            layoutGroup.padding.left = layoutGroup.padding.right = padding;
            scroll.horizontalNormalizedPosition = prevLevelIcon.index / (index - 1);
        }
        public void SelectLevel(LevelIcon icon)
        {
            if (prevLevelIcon != null)
                prevLevelIcon.Disappear();
            prevLevelIcon = icon;
            if (!icon.isLock)
            {
                gameManager.curLevelIndex = icon.index;
                title.text = "Selected stage: " + (icon.index + 1);
            }
            Color c;
            if (gameManager.curLevel.starRating >= 5)
                c = Color.red;
            else if (gameManager.curLevel.starRating >= 3)
                c = Color.yellow;
            else c = Color.cyan;
            title.color = c;
        }
    }
}