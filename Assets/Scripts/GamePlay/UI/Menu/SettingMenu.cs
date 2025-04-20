using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class SettingMenu : Menu
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle muteCheckbox;

        public override void Awake()
        {
            base.Awake();
            soundSlider.onValueChanged.AddListener(val => gameManager.soundVolume = val);
            sfxSlider.onValueChanged.AddListener(val => gameManager.sfxVolume = val);
            muteCheckbox.onValueChanged.AddListener(val => gameManager.isMute = val);
        }
        public void OnEnable()
        {
            soundSlider.SetValueWithoutNotify(gameManager.soundVolume);
            sfxSlider.SetValueWithoutNotify(gameManager.sfxVolume);
            muteCheckbox.SetIsOnWithoutNotify(gameManager.isMute);
        }
    }
}