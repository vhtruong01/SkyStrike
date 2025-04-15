using SkyStrike.Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public class LevelMenu : Menu
        {
            [SerializeField] private GameManager gameManager;
            [SerializeField] private Transform container;
            [SerializeField] private LevelCard levelCardPrefab;
            public UnityAction closeAction { private get; set; }

            public override void Start()
            {
                closeBtn.onClick.RemoveAllListeners();
                for (int i = 0; i < gameManager.levelDataList.Length; i++)
                {
                    var levelData = gameManager.levelDataList[i];
                    if (levelData == null) continue;
                    var card = Instantiate(levelCardPrefab, container, false);
                    card.Init(levelData, () => PlayGame(levelData));
                }
                closeBtn.onClick.AddListener(closeAction);
            }
            private void PlayGame(LevelData levelData)
            {
                gameManager.curLevel = levelData;
                GameManager.PlayGame();
            }
        }
    }
}