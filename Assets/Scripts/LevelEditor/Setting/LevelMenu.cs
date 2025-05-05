using UnityEngine;

namespace SkyStrike.Editor
{
    public class LevelMenu : Menu
    {
        [SerializeField] private StringProperty levelName;
        [SerializeField] private IntProperty starRating;

        protected override void Preprocess()
            => EventManager.onSelectLevel.AddListener(SelectLevel);
        private void SelectLevel(LevelDataObserver levelData)
        {
            starRating.Unbind();
            levelName.Unbind();
            starRating.Bind(levelData.starRating);
            levelName.Bind(levelData.levelName);
        }
    }
}