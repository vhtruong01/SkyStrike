using SkyStrike.Game;
using UnityEngine;

namespace SkyStrike.UI
{
    public class LevelMenu : Menu
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Transform container;
        [SerializeField] private LevelCard levelCardPrefab;
        [SerializeField] private MainMenu mainMenu;

        public override void Start()
        {
            closeBtn.onClick.RemoveAllListeners();
            for (int i = 0; i < gameManager.levelDataList.Length; i++)
            {
                var levelData = gameManager.levelDataList[i];
                if (levelData == null) continue;
                var card = Instantiate(levelCardPrefab, container, false);
                card.Init(levelData, () => StartCoroutine(mainMenu.PlayGame(levelData)));
            }
            closeBtn.onClick.AddListener(() => StartCoroutine(mainMenu.CloseLevelMenu()));
        }
    }
}