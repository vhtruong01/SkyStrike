using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MainGame : MonoBehaviour
        {
            [SerializeField] private ObjectManager objectManager;
            public static LevelData Level { get; set; }
            private int waveIndex;

            public void Start() => Restart();
            public void Restart()
            {
                print("start game");
                Level ??= Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
                waveIndex = 0;
                StartWave();
            }
            private void StartWave()
            {
                if (waveIndex >= Level.waves.Length)
                {
                    print("end game");
                    return;
                }
                objectManager.CreateWave(Level.waves[waveIndex]);
                waveIndex++;
            }
            private void RecallObject()
            {

            }
        }
    }
}