using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class ObjectManager : PoolManager<Enemy>
        {
            [SerializeField] private List<MetaData> metaDataList;
            private Dictionary<int, MetaData> metaDataDict;
            Dictionary<int, ObjectData> objectDataDict;

            public override void Awake()
            {
                base.Awake();
                objectDataDict = new();
                metaDataDict = new();
                foreach (var metaData in metaDataList)
                    metaDataDict.Add(metaData.id, metaData);
            }
            public void CreateWave(WaveData waveData)
            {
                objectDataDict.Clear();
                foreach (var objectData in waveData.objectDataArr)
                    objectDataDict.Add(objectData.id, objectData);
                foreach (var objectData in waveData.objectDataArr)
                {
                    objectData.metaData = metaDataDict[objectData.metaId];
                    Enemy enemy = CreateItem();
                    if (objectData.refId != -1)
                    {
                        MoveData oldData = objectData.moveData;
                        Vec2 pos = oldData.points[0].midPos;
                        objectData.moveData = GetMoveDataClone(objectData.refId);
                        objectData.moveData.Translate(pos);
                        objectData.moveData.velocity = oldData.velocity;
                        objectData.moveData.delay = oldData.delay;
                    }
                    enemy.SetData(objectData);
                    enemy.Strike(objectData.moveData);
                    float delay = objectData.moveData.delay;
                    Enemy cloneEnemy;
                    for (int i = 1; i <= objectData.cloneCount; i++)
                    {
                        cloneEnemy = CreateItem();
                        cloneEnemy.SetData(objectData);
                        cloneEnemy.Strike(objectData.moveData, i);
                    }
                }
            }
            private MoveData GetMoveDataClone(int refId)
            {
                ObjectData refObject = objectDataDict.GetValueOrDefault(refId);
                if (refObject.refId == -1)
                    return refObject.moveData.Clone();
                return GetMoveDataClone(refObject.refId);
            }
        }
    }
}