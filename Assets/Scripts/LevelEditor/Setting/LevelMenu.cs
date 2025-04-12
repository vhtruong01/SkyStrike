using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class LevelMenu : Menu
        {
            [SerializeField] private StringProperty levelName;
            [SerializeField] private IntProperty star;

            public override void Awake()
            {
                base.Awake();
                EventManager.onSelectLevel.AddListener(SelectLevel);
            }
            private void SelectLevel(LevelDataObserver levelData)
            {
                star.Unbind();
                levelName.Unbind();
                star.Bind(levelData.star);
                levelName.Bind(levelData.levelName);
            }
            public override void Init() { }
            protected override void CreateObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) { }
            protected override void SelectObject(ObjectDataObserver data) { }
            protected override void SelectWave(WaveDataObserver data) { }
        }
    }
}