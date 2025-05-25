using SkyStrike.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LevelMenu : Menu
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TextMeshProUGUI starTxt;
        [SerializeField] private LevelIcon levelIcon0Prefab;
        [SerializeField] private LevelIcon levelIcon1Prefab;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Transform line;
        [SerializeField] private float lineSpeed = 3f;
        [SerializeField] private StartGameButton startBtn;
        private readonly float duration = 10;
        private float elapsedTime;
        private Vector3 originalLinePos;

        private LevelIcon prevLevelIcon;

        public override void Start()
        {
            starTxt.text = gameManager.star.ToString();
            var scroll = GetComponentInChildren<ScrollRect>();
            int index = 0;
            for (int i = 0; i < gameManager.levelDataList.Count; i++)
            {
                var levelData = gameManager.levelDataList[i];
                if (levelData == null) continue;
                var icon = Instantiate(index % 2 == 0 ? levelIcon0Prefab : levelIcon1Prefab, scroll.content.transform, false);
                icon.isLock = i > gameManager.maxLevel;
                icon.Init(levelData, index, gameManager.GetScore(index), SelectLevel);
                if (index == gameManager.curLevelIndex)
                    icon.Appear();
                index++;
            }
            var layoutGroup = scroll.content.GetComponentInChildren<HorizontalOrVerticalLayoutGroup>();
            float scrollW = scroll.GetComponent<RectTransform>().rect.width;
            float iconW = levelIcon0Prefab.GetComponent<RectTransform>().rect.width;
            int padding = (int)((scrollW - iconW) / 2);
            layoutGroup.padding.left = layoutGroup.padding.right = padding;
            scroll.horizontalNormalizedPosition = 1f * prevLevelIcon.index / (index - 1);
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
            startBtn.Enable(!icon.isLock);
            if (!icon.isLock)
            {
                gameManager.curLevelIndex = icon.index;
                title.text = "Selected stage: " + (icon.index + 1);
            }
            title.color = gameManager.curLevel.starRating switch
            {
                0 => Color.grey,
                1 => Color.green,
                2 => Color.cyan,
                3 => Color.yellow,
                4 => new(1, 0.5f, 0),
                5 => Color.magenta,
                _ => Color.red,
            };
        }
    }
}