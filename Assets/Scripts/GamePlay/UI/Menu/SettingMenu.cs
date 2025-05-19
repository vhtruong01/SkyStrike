using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class SettingMenu : Menu
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle muteCheckbox;

        public override void Awake()
        {
            base.Awake();
            soundSlider.onValueChanged.AddListener(val => soundManager.soundVolume = val);
            sfxSlider.onValueChanged.AddListener(val => soundManager.sfxVolume = val);
            muteCheckbox.onValueChanged.AddListener(val => soundManager.isMute = val);
        }
        public void OnEnable()
        {
            soundSlider.SetValueWithoutNotify(soundManager.soundVolume);
            sfxSlider.SetValueWithoutNotify(soundManager.sfxVolume);
            muteCheckbox.SetIsOnWithoutNotify(soundManager.isMute);
        }
    }
}