using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MainGame : MonoBehaviour
        {
            [SerializeField] private ObjectManager objectManager;
            private LevelData level;
            private int waveIndex;

            public void Awake()
            {
                EventManager.onRemoveEnemy.AddListener(RecallObject);
            }
            public void Start() => Restart();
            public void OpenEditor() => GameManager.LoadScene(EScene.Editor);
            public void Restart()
            {
                print("start game");
                level = Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
                waveIndex = 0;
                StartWave();
            }
            private void StartWave()
            {
                if (waveIndex >= level.waves.Length)
                {
                    print("end game");
                    return;
                }
                objectManager.CreateWave(level.waves[waveIndex]);
                waveIndex++;
            }
            private void RecallObject(Enemy enemy)
            {
                objectManager.RemoveItem(enemy);
                if (objectManager.enemyCount == 0)
                    StartWave();
            }
        }
    }
}