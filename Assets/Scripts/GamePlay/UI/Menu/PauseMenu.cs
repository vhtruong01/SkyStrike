using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class PauseMenu : Menu
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle muteCheckbox;
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button mainMenuBtn;

        public override void Awake()
        {
            base.Awake();
            soundSlider.onValueChanged.AddListener(val => gameManager.soundVolume = val);
            sfxSlider.onValueChanged.AddListener(val => gameManager.sfxVolume = val);
            muteCheckbox.onValueChanged.AddListener(val => gameManager.isMute = val);
            resumeBtn.onClick.AddListener(Collapse);
            restartBtn.onClick.AddListener(SceneSwapper.PlayGame);
            mainMenuBtn.onClick.AddListener(SceneSwapper.OpenMainMenu);
        }
        public void OnEnable()
        {
            soundSlider.SetValueWithoutNotify(gameManager.soundVolume);
            sfxSlider.SetValueWithoutNotify(gameManager.sfxVolume);
            muteCheckbox.SetIsOnWithoutNotify(gameManager.isMute);
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