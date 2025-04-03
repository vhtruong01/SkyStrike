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

            public override void Awake()
            {
                base.Awake();
                metaDataDict = new();
                foreach (var metaData in metaDataList)
                    metaDataDict.Add(metaData.id, metaData);
            }
            public void CreateWave(WaveData waveData)
            {
                Dictionary<int, ObjectData> objectDataDict = new();
                foreach (var objectData in waveData.objectDataArr)
                        objectDataDict.Add(objectData.id, objectData);
                foreach (var objectData in waveData.objectDataArr)
                {
                    objectData.metaData = metaDataDict[objectData.metaId];
                    Enemy enemy = CreateItem();
                    if (objectData.refId != -1)
                    {
                        MoveData refMoveData = objectDataDict[objectData.refId].moveData;
                        Vec2 pos = objectData.moveData.points[0].midPos;
                        objectData.moveData = refMoveData.Clone(pos);
                    }
                    enemy.SetData(objectData);
                }
            }
        }
    }
}