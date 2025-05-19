using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class PauseMenu : Menu
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle muteCheckbox;
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button mainMenuBtn;

        public override void Awake()
        {
            base.Awake();
            soundSlider.onValueChanged.AddListener(val => soundManager.soundVolume = val);
            sfxSlider.onValueChanged.AddListener(val => soundManager.sfxVolume = val);
            muteCheckbox.onValueChanged.AddListener(val => soundManager.isMute = val);
            resumeBtn.onClick.AddListener(Collapse);
            restartBtn.onClick.AddListener(SceneSwapper.PlayGame);
            mainMenuBtn.onClick.AddListener(SceneSwapper.OpenMainMenu);
        }
        public void OnEnable()
        {
            soundSlider.SetValueWithoutNotify(soundManager.soundVolume);
            sfxSlider.SetValueWithoutNotify(soundManager.sfxVolume);
            muteCheckbox.SetIsOnWithoutNotify(soundManager.isMute);
        }
        public override void Collapse()
        {
            base.Collapse();
            Time.timeScale = 1;
        }
        public override void Expand()
        {
            base.Expand();
            Time.timeScale = 0;
        }
    }
}