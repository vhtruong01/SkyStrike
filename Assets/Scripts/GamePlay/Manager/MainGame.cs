using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MainGame : MonoBehaviour
        {
            public static LevelData Level{ get; set; }
            [SerializeField] private GameObjectManager gameObjectPool;

            public void Awake()
            {
                Level = Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
            }
            public void Start() => StartCoroutine(PlayGame());
            public IEnumerator PlayGame()
            {
                var lv = Level;
                if (lv == null) yield break;
                print("start game");
                for (int i = 0; i < lv.waves.Length; i++)
                    yield return StartCoroutine(StartWave(lv.waves[i]));
                print("end game");
            }
            public IEnumerator StartWave(WaveData wave)
            {
                if (wave.delay > 0)
                    yield return new WaitForSeconds(wave.delay);
                List<Coroutine> coroutines = new();
                for (int i = 0; i < wave.objectDataArr.Length; i++)
                {
                    var item = gameObjectPool.CreateItem(wave.objectDataArr[i]);
                    Coroutine coroutine = StartCoroutine(item.Appear());
                    coroutines.Add(coroutine);
                }
                foreach (Coroutine coroutine in coroutines)
                    yield return coroutine;
            }
        }
    }
}