using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MainGame : MonoBehaviour
        {
            [SerializeField] private EnemyManager enemyManager;

            public void Start() => Restart();
            public void Restart()
            {
                print("start game");
                var level = Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
                enemyManager.Play(level.waves);
            }
            public void OpenEditor() => GameManager.LoadScene(EScene.Editor);
        }
    }
}