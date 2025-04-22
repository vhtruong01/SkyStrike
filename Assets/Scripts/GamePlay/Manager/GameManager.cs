using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkyStrike.Game
{
    public enum EScene
    {
        Loading = 0,
        MainMenu,
        MainGame,
        Editor
    }
    [CreateAssetMenu(fileName = "GameManager", menuName = "Game/GameManager")]
    public class GameManager : ScriptableObject
    {
        [SerializeField, Range(0, 1)] private float _soundVolume;
        [SerializeField, Range(0, 1)] private float _sfxVolume;
        [SerializeField] private bool _isMute;
        public LevelData curLevel { get; set; }
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
        public void OnEnable() => Awake();
        public void Awake()
        {
            _soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.75f);
            _sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
            _isMute = PlayerPrefs.GetInt("isMute", 1) == 0;
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Levels");
            if (textAssets.Length <= 0) return;
            levelDataList = new LevelData[textAssets.Length];
            for (int i = 0; i < textAssets.Length; i++)
                levelDataList[i] = ReadTextAssetFile(textAssets[i]);
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
        private static void LoadScene(EScene sceneType)
            => SceneManager.LoadScene((int)sceneType);
        public static void PlayGame() => LoadScene(EScene.MainGame);
        public static void OpenMainMenu() => LoadScene(EScene.MainMenu);
        public static void OpenEditor() => LoadScene(EScene.Editor);
    }
}