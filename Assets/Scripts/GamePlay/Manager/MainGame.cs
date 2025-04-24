using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bg1;
        [SerializeField] private ParticleSystem bg2;
        private LevelData levelData;
        private int waveIndex;
        private Coroutine nextWaveCoroutine;
        private Dictionary<int, EnemyBulletMetaData> bulletDataDict;
        private Dictionary<int, ObjectData> objectDataDict;

        public void Awake()
        {
            bg1.Stop();
            bg2.Stop();
            levelData = Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
            bulletDataDict = new();
            objectDataDict = new();
            EventManager.onPlayNextWave.AddListener(StartNextWave);
        }
        public void Restart()
        {
            print("start game");
            bg1.Play();
            bg2.Play();
            waveIndex = 0;
            objectDataDict.Clear();
            bulletDataDict.Clear();
            if (levelData.bullets != null)
                foreach (var bullet in levelData.bullets)
                    bulletDataDict.Add(bullet.id, bullet);
            StartWave();
        }
        public void StartWave()
        {
            if (waveIndex >= levelData.waves.Length)
            {
                print("end game");
                return;
            }
            print("wave: " + (1 + waveIndex));
            //if (waves[waveIndex].isBoss)
            WaveData waveData = levelData.waves[waveIndex];
            foreach (var objectData in waveData.objectDataArr)
                objectDataDict.Add(objectData.id, objectData);
            foreach (var objectData in waveData.objectDataArr)
            {
                foreach (var point in objectData.moveData.points)
                {
                    point.bulletData = bulletDataDict.GetValueOrDefault(point.bulletId);
                    if (point.bulletData != null)
                        point.bulletData.color = gameObject.RandomColor();
                }
                if (objectData.refId != -1)
                    objectData.CopyMoveData(CloneMoveData(objectData.refId));
                int randomEnemyIndex = objectData.dropItemType == EItem.None ? -1 : Random.Range(0, objectData.cloneCount + 1);
                for (int i = 0; i <= objectData.cloneCount; i++)
                {
                    EItem dropItemType;
                    if (randomEnemyIndex == i)
                        dropItemType = objectData.dropItemType;
                    else dropItemType = EItem.None;
                    float delay = waveData.delay + objectData.moveData.delay + i * objectData.spawnInterval;
                    EventManager.CreateEnemy(objectData, dropItemType, delay);
                }
            }
            float waveDuration = waveData.objectDataArr.Length != 0 ? waveData.duration : 1;
            nextWaveCoroutine = StartCoroutine(WaitForNextWave(waveDuration));
        }
        private IEnumerator WaitForNextWave(float time)
        {
            if (time <= 0) yield break;
            yield return new WaitForSeconds(time);
            nextWaveCoroutine = null;
            StartNextWave();
        }
        private void StartNextWave()
        {
            if (nextWaveCoroutine != null)
                StopCoroutine(nextWaveCoroutine);
            waveIndex++;
            StartWave();
        }
        private MoveData CloneMoveData(int refId)
        {
            ObjectData refObject = objectDataDict.GetValueOrDefault(refId);
            if (refObject.refId == -1)
                return refObject.moveData.Clone();
            return CloneMoveData(refObject.refId);
        }
    }
}