using UnityEngine;

namespace SkyStrike.Editor
{
    public class LevelMenu : Menu
    {
        [SerializeField] private StringProperty levelName;
        [SerializeField] private IntProperty starRating;

        public override void Init()
        {
            EventManager.onSelectLevel.AddListener(SelectLevel);
        }
        private void SelectLevel(LevelDataObserver levelData)
        {
            starRating.Unbind();
            levelName.Unbind();
            starRating.Bind(levelData.starRating);
            levelName.Bind(levelData.levelName);
        }
    }
}