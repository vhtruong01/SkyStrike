using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyManager : PoolManager<Enemy, ObjectData>
        {
            [SerializeField] private List<EnemyMetaData> metaDataList;
            private Dictionary<int, EnemyMetaData> metaDataDict;
            private Dictionary<int, EnemyBulletData> bulletDataDict;
            private Dictionary<int, ObjectData> objectDataDict;
            private WaveData[] waves;
            private int waveIndex;
            private float z;

            public void Play(LevelData levelData)
            {
                waves = levelData.waves;
                objectDataDict = new();
                metaDataDict = new();
                bulletDataDict = new();
                z = transform.position.z;
                foreach (var metaData in metaDataList)
                    metaDataDict.Add(metaData.id, metaData);
                foreach (var bullet in levelData.bullets)
                    bulletDataDict.Add(bullet.id, bullet);
                waveIndex = 0;
                StartWave();
            }
            public void StartWave()
            {
                if (waveIndex >= waves.Length)
                {
                    print("end game");
                    return;
                }
                print("wave: " + waveIndex);
                WaveData waveData = waves[waveIndex];
                objectDataDict.Clear();
                foreach (var objectData in waveData.objectDataArr)
                    objectDataDict.Add(objectData.id, objectData);
                foreach (var objectData in waveData.objectDataArr)
                {
                    objectData.metaData = metaDataDict[objectData.metaId];
                    foreach (var point in objectData.moveData.points)
                    {
                        point.bulletDataList = new EnemyBulletData[point.bulletIdArr.Length];
                        for (int i = 0; i < point.bulletIdArr.Length; i++)
                            point.bulletDataList[i] = bulletDataDict[point.bulletIdArr[i]];
                    }
                    if (objectData.refId != -1)
                    {
                        MoveData oldData = objectData.moveData;
                        Vec2 pos = oldData.points[0].midPos;
                        objectData.moveData = CloneMoveData(objectData.refId);
                        objectData.moveData.Translate(pos);
                        objectData.moveData.velocity = oldData.velocity;
                        objectData.moveData.delay = oldData.delay;
                    }
                    Enemy cloneEnemy;
                    int randomEnemyIndex = objectData.dropItemType == EItem.None ? -1 : Random.Range(0, objectData.cloneCount + 1);
                    for (int i = 0; i <= objectData.cloneCount; i++)
                    {
                        cloneEnemy = InstantiateItem(objectData, objectData.pos.SetZ(z));
                        cloneEnemy.Strike(waveData.delay, i);
                        if (randomEnemyIndex == i)
                            cloneEnemy.dropItemType = objectData.dropItemType;
                        else cloneEnemy.dropItemType = EItem.None;
                    }
                }
                StartCoroutine(WaitForNextWave(waveData.duration));
            }
            private IEnumerator WaitForNextWave(float time)
            {
                if (time <= 0) yield break;
                yield return new WaitForSeconds(time);
                StartNextWave();
            }
            private void StartNextWave()
            {
                waveIndex++;
                StartWave();
            }
            protected override void DestroyItem(Enemy enemy)
            {
                base.DestroyItem(enemy);
                if (pool.CountActive == 0)
                    StartNextWave();
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
}