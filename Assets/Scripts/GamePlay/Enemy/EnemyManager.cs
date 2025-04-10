using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class EnemyManager : PoolManager<Enemy, ObjectData>
        {
            [SerializeField] private List<MetaData> metaDataList;
            private Dictionary<int, MetaData> metaDataDict;
            private Dictionary<int, ObjectData> objectDataDict;
            private WaveData[] waves;
            private int waveIndex;
            private float z;

            public override void Awake()
            {
                base.Awake();
                objectDataDict = new();
                metaDataDict = new();
                z = transform.position.z;
                foreach (var metaData in metaDataList)
                    metaDataDict.Add(metaData.id, metaData);
            }
            public void Play(WaveData[] waves)
            {
                this.waves = waves;
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
                WaveData waveData = waves[waveIndex];
                objectDataDict.Clear();
                foreach (var objectData in waveData.objectDataArr)
                    objectDataDict.Add(objectData.id, objectData);
                foreach (var objectData in waveData.objectDataArr)
                {
                    objectData.metaData = metaDataDict[objectData.metaId];
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
                    int enemyIndex = objectData.dropItemType == EItem.None ? -1 : Random.Range(0, objectData.cloneCount + 1);
                    for (int i = 0; i <= objectData.cloneCount; i++)
                    {
                        cloneEnemy = InstantiateItem(objectData, objectData.pos.SetZ(z));
                        cloneEnemy.Strike(objectData.moveData, waveData.delay, i);
                        if (enemyIndex == i)
                            cloneEnemy.dropItemType = objectData.dropItemType;
                        else cloneEnemy.dropItemType = EItem.None;
                    }
                }
            }
            protected override void DestroyItem(Enemy enemy)
            {
                base.DestroyItem(enemy);
                if (pool.CountActive == 0)
                {
                    waveIndex++;
                    StartWave();
                }
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