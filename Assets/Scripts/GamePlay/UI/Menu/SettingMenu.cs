using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class SettingMenu : Menu
    {
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle muteCheckbox;

        public override void Awake()
        {
            base.Awake();
            soundSlider.onValueChanged.AddListener(val => SoundManager.soundVolume = val);
            sfxSlider.onValueChanged.AddListener(val => SoundManager.sfxVolume = val);
            muteCheckbox.onValueChanged.AddListener(val => SoundManager.isMute = val);
        }
        public void OnEnable()
        {
            soundSlider.SetValueWithoutNotify(SoundManager.soundVolume);
            sfxSlider.SetValueWithoutNotify(SoundManager.sfxVolume);
            muteCheckbox.SetIsOnWithoutNotify(SoundManager.isMute);
        }
    }
}