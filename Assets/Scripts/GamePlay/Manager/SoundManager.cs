using UnityEngine;

namespace SkyStrike.Game
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        [SerializeField, Range(0, 1)] private float _soundVolume;
        [SerializeField, Range(0, 1)] private float _sfxVolume;
        [SerializeField] private bool _isMute;
        [SerializeField] private AudioSource bgmSound;
        [SerializeField] private AudioSource sfxSound;
        public float soundVolume
        {
            get => _soundVolume;
            set
            {
                _soundVolume = value;
                bgmSound.volume = _soundVolume;
                PlayerPrefs.SetFloat("soundVolume", value);
            }
        }
        public float sfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                sfxSound.volume = _sfxVolume;
                PlayerPrefs.SetFloat("sfxVolume", value);
            }
        }
        public bool isMute
        {
            get => _isMute;
            set
            {
                _isMute = value;
                bgmSound.mute = sfxSound.mute = isMute;
                PlayerPrefs.SetFloat("isMute", isMute ? 0 : 1);
            }
        }
        public void Awake()
        {
            soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.75f);
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
            isMute = PlayerPrefs.GetInt("isMute", 1) == 0;
            instance = this;
        }
    }
}