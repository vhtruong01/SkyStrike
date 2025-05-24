using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum ESound
    {
        None = 0,
        Star1,
        Star5,
        Health,
        Energy,
        LostItem,
        CollectItem,

        WeaponUpgrade = 10,
        Shield,
        MegaBomb,
        Laser,

        SingleBullet = 30,
        DoubleBullet,
        TripleBullet,
        Missile,

        ShipHit1 = 50,
        ShipHit2,
        ShipHit3,
        ShieldStart,
        ShieldEnd,
        LaserStart,
        LowHp,
        Clock,
        HpWarning,
        SpecialVoice1,
        SpecialVoice2,

        EnemyHit = 70,
        EnemyDie1,
        EnemyDie2,
        EnemyDie3,
        EnemyDie4,
        EnemyShot1,
        EnemyShot2,
        EnemyShot3,
        EnemyShot4,
        EnemyShot5,
        EnemyShot6,

        MainMenu = 100,
        Boss,
        Stage1,
        Stage2,
        Stage3,
        Stage4,

        StageComplete = 200,
        GameOver,
        MissionBegin,
        SummaryMultiple,
        SummaryStar,
        Win,
        Lose,
        LevelUp2,
        LevelUp3,
        LevelUp4,
        LevelUp5,
        LevelUp6,
        LevelUp7,
        LevelUp8,
        LevelUp9,
        LevelUp10,
        LevelUp11,


        Button = 300,
        CloseButton,
        LevelIcon,
        PauseButton,
        StartGameButton,
        CollectButton,
        Text,
        BossWarning,
        Explosion,
    }
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        [SerializeField, Range(0, 1)] private float _soundVolume;
        [SerializeField, Range(0, 1)] private float _sfxVolume;
        [SerializeField] private bool _isMute;
        [SerializeField] private AudioSource bgmSound;
        [SerializeField] private AudioSource sfxSound;
        [SerializeField] private List<SoundData> soundDataList;
        private Dictionary<ESound, SoundData> soundDict;

        public void Awake()
        {
            instance = this;
            soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.33f);
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
            isMute = PlayerPrefs.GetInt("isMute", 1) == 0;
            soundDict = new();
            foreach (var item in soundDataList)
                soundDict.Add(item.eSound, item);
        }
        private void PlayBgm(AudioClip audioClip)
        {

            bgmSound.clip = audioClip;
            bgmSound.Play();
        }
        private void PlaySfx(AudioClip audioClip)
                => sfxSound.PlayOneShot(audioClip);
        public static void PlaySound(ESound eSound)
        {
            if (instance == null || !instance.soundDict.TryGetValue(eSound, out var s) || eSound == ESound.None) return;
            if (s.isBgm)
                instance.PlayBgm(s.audioClip);
            else instance.PlaySfx(s.audioClip);
        }
        public static float soundVolume
        {
            get => instance._soundVolume;
            set
            {
                instance._soundVolume = value;
                instance.bgmSound.volume = instance._soundVolume;
                PlayerPrefs.SetFloat("soundVolume", value);
            }
        }
        public static float sfxVolume
        {
            get => instance._sfxVolume;
            set
            {
                instance._sfxVolume = value;
                instance.sfxSound.volume = instance._sfxVolume;
                PlayerPrefs.SetFloat("sfxVolume", value);
            }
        }
        public static bool isMute
        {
            get => instance._isMute;
            set
            {
                instance.bgmSound.mute = instance.sfxSound.mute = instance._isMute = value;
                PlayerPrefs.SetInt("isMute", value ? 0 : 1);
            }
        }
        public static void Pause()
        {
            instance.bgmSound.Stop();
            instance.sfxSound.Stop();
        }
        public static void Resume()
        {
            instance.bgmSound.Play();
            instance.sfxSound.Play();
        }
        public static void StopBgm()
        {
            instance.bgmSound.Stop();
        }

        [Serializable]
        private class SoundData
        {
            public ESound eSound;
            public AudioClip audioClip;
            public bool isBgm;
            public bool isLoop;
        }
    }
}