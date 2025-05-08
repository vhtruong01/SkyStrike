using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
    public class GameManager : ScriptableObject
    {
        [SerializeField, Range(0, 1)] private float _soundVolume;
        [SerializeField, Range(0, 1)] private float _sfxVolume;
        [SerializeField] private bool _isMute;
        public int curLevelIndex { get; set; }
        public LevelData curLevel => levelDataList[curLevelIndex];
        public LevelData[] levelDataList { get; private set; }
        public float soundVolume
        {
            get => _soundVolume;
            set
            {
                _soundVolume = value;
                PlayerPrefs.SetFloat("soundVolume", value);
            }
        }
        public float sfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                PlayerPrefs.SetFloat("sfxVolume", value);
            }
        }
        public bool isMute
        {
            get => _isMute;
            set
            {
                _isMute = value;
                PlayerPrefs.SetFloat("isMute", isMute ? 0 : 1);
            }
        }

        //
        public void OnEnable() => Awake();
        private void Awake()
        {
            _soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.75f);
            _sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
            _isMute = PlayerPrefs.GetInt("isMute", 1) == 0;
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Levels");
            if (textAssets.Length <= 0) return;
            levelDataList = new LevelData[textAssets.Length];
            for (int i = 0; i < textAssets.Length; i++)
                levelDataList[i] = ReadTextAssetFile(textAssets[i]);
            curLevelIndex = Mathf.Min(levelDataList.Length - 1, PlayerPrefs.GetInt("curLevel", 0));
        }
        private LevelData ReadTextAssetFile(TextAsset data)
        {
            try
            {
                using MemoryStream stream = new(data.bytes);
                return new BinaryFormatter().Deserialize(stream) as LevelData;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }
    }
}