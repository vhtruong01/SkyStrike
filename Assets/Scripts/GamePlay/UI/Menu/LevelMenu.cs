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
        [SerializeField] private Transform line;
        [SerializeField] private float lineSpeed = 3f;
        private readonly float duration = 10;
        private float elapsedTime;
        private Vector3 originalLinePos;

        private LevelIcon prevLevelIcon;

        public override void Start()
        {
            var scroll = GetComponentInChildren<ScrollRect>();
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
            originalLinePos = line.position;
        }
        public void Update()
        {
            line.position += new Vector3(0, -Time.deltaTime * lineSpeed, 0);
            elapsedTime -= Time.deltaTime;
            if (elapsedTime < 0)
            {
                elapsedTime = duration;
                line.position = originalLinePos;
            }
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