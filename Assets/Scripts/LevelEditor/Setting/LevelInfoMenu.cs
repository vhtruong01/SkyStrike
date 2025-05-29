using UnityEngine;

namespace SkyStrike.Editor
{
    public class LevelInfoMenu : Menu
    {
        [SerializeField] private StringProperty levelName;
        [SerializeField] private IntProperty starRating;
        [SerializeField] private BoolProperty isUseNightBugTheme;

        protected override void Preprocess()
            => EventManager.onSelectLevel.AddListener(SelectLevel);
        private void SelectLevel(LevelDataObserver levelData)
        {
            starRating.Unbind();
            levelName.Unbind();
            isUseNightBugTheme.Unbind();
            starRating.Bind(levelData.starRating);
            levelName.Bind(levelData.levelName);
            isUseNightBugTheme.Bind(levelData.isUseNightBugTheme);
        }
    }
}