using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class LevelManager : MonoBehaviour
    {
        private readonly EnemyData.EnemyEventData enemyEventData = new();
        private Dictionary<int, EnemyBulletMetaData> bulletDataDict;
        private Dictionary<int, ObjectData> objectDataDict;
        private Coroutine nextWaveCoroutine;
        private LevelData levelData;
        private int waveIndex;

        public void Awake()
        {
            bulletDataDict = new();
            objectDataDict = new();
        }
        public void Start()
        {
            //
            levelData = Editor.Controller.ReadFromBinaryFile<LevelData>("test.dat");
        }
        private void OnEnable()
            => EventManager.Subscribe(EEventType.PlayNextWave, StartNextWave);
        private void OnDisable()
            => EventManager.Unsubscribe(EEventType.PlayNextWave, StartNextWave);
        public void Restart()
        {
            print("start game");
            waveIndex = 0;
            objectDataDict.Clear();
            bulletDataDict.Clear();
            if (levelData.bullets != null)
                foreach (var bullet in levelData.bullets)
                    bulletDataDict.Add(bullet.id, bullet);
            StartWave();
        }
        private void StartWave()
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
                enemyEventData.isMaintain = objectData.isMaintain;
                enemyEventData.size = objectData.size;
                enemyEventData.position = objectData.pos.SetZ(0);
                enemyEventData.moveData = objectData.moveData;
                enemyEventData.metaId = objectData.metaId;
                for (int i = 0; i <= objectData.cloneCount; i++)
                {
                    enemyEventData.dropItemType = randomEnemyIndex == i ? objectData.dropItemType : EItem.None;
                    enemyEventData.delay = waveData.delay + objectData.moveData.delay + i * objectData.spawnInterval;
                    EventManager.Active(enemyEventData);
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