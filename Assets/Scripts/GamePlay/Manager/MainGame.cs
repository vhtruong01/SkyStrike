using UnityEngine;

namespace SkyStrike.Game
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private ParticleSystem bg1;
        [SerializeField] private ParticleSystem bg2;
        private LevelData levelData;

        public void Awake()
        {
            bg1.Stop();
            bg2.Stop();
            levelData = Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
        }
        public void Restart()
        {
            print("start game");
            bg1.Play();
            bg2.Play();
            enemyManager.Play(levelData);
        }
    }
}