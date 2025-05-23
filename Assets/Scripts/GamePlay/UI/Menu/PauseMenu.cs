using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class PauseMenu : Menu
    {
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle muteCheckbox;
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button mainMenuBtn;

        public override void Start()
        {
            base.Start();
            soundSlider.onValueChanged.AddListener(val => SoundManager.soundVolume = val);
            sfxSlider.onValueChanged.AddListener(val => SoundManager.sfxVolume = val);
            muteCheckbox.onValueChanged.AddListener(val => SoundManager.isMute = val);
            resumeBtn.onClick.AddListener(Collapse);
            restartBtn.onClick.AddListener(SceneSwapper.PlayGame);
            mainMenuBtn.onClick.AddListener(SceneSwapper.OpenMainMenu);
        }
        public void OnEnable()
        {
            soundSlider.SetValueWithoutNotify(SoundManager.soundVolume);
            sfxSlider.SetValueWithoutNotify(SoundManager.sfxVolume);
            muteCheckbox.SetIsOnWithoutNotify(SoundManager.isMute);
        }
        public override void Collapse()
        {
            base.Collapse();
            Time.timeScale = 1;
            SoundManager.Resume();
        }
        public override void Expand()
        {
            base.Expand();
            Time.timeScale = 0;
            SoundManager.Pause();
        }
    }
}