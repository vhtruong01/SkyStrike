using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class MainGame : MonoBehaviour
        {
            public static LevelData Level { get; set; }
            [SerializeField] private GameObjectManager gameObjectPool;
            private Dictionary<int, ObjectData> objectDataDict;

            public void Awake()
            {
                objectDataDict = new();
                Level ??= Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
            }
            public void Start() => Restart();
            public void Restart()
            {
                StopAllCoroutines();
                gameObjectPool.Clear();
                StartCoroutine(PlayGame());
            }
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
                objectDataDict.Clear();
                for (int i = 0; i < wave.objectDataArr.Length; i++)
                {
                    var itemData = wave.objectDataArr[i];
                    objectDataDict.Add(itemData.id, itemData);
                }
                for (int i = 0; i < wave.objectDataArr.Length; i++)
                {
                    var itemData = wave.objectDataArr[i];
                    if (objectDataDict.TryGetValue(itemData.refId, out var refObjectData))
                        itemData.phase = refObjectData.phase;
                    var item = gameObjectPool.CreateItem(itemData);
                    Coroutine coroutine = StartCoroutine(item.Appear());
                    coroutines.Add(coroutine);
                }
                foreach (Coroutine coroutine in coroutines)
                    yield return coroutine;
            }
        }
    }
}