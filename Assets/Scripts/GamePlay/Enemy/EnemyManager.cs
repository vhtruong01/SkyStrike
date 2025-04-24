using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(EnemyBulletManager))]
    public class EnemyManager : PoolManager<Enemy, EnemyData>
    {
        [SerializeField] private List<EnemyMetaData> metaDataList;
        private Dictionary<int, EnemyMetaData> metaDataDict;

        public override void Awake()
        {
            base.Awake();
            metaDataDict = new();
            foreach (var metaData in metaDataList)
                metaDataDict.Add(metaData.id, metaData);
            EventManager.onCreateEnemy.AddListener(CreateEnemy);
        }
        protected void CreateEnemy(ObjectData objectData, EItem itemType, float delay)
        {
            Enemy enemy = InstantiateItem(objectData.pos.SetZ(0));
            enemy.data.SetExtraData(objectData, itemType);
            enemy.data.UpdateDataAndRefresh(metaDataDict[objectData.metaId]);
            enemy.Strike(delay);
        }
        protected override void DestroyItem(Enemy enemy)
        {
            base.DestroyItem(enemy);
            if (pool.CountActive == 0)
                EventManager.PlayNextWave();
        }
    }
}