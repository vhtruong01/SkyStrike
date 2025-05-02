using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
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
        }
        private void OnEnable()
            => EventManager.Subscribe<EnemyData.EnemyEventData>(CreateEnemy);
        private void OnDisable()
            => EventManager.Unsubscribe<EnemyData.EnemyEventData>(CreateEnemy);
        private void CreateEnemy(EnemyData.EnemyEventData eventData)
        {
            eventData.metaData = metaDataDict[eventData.metaId];
            Enemy enemy = InstantiateItem(eventData.position);
            enemy.data.SetData(eventData);
            enemy.Launch(eventData.delay);
        }
        protected override void DestroyItem(Enemy enemy)
        {
            base.DestroyItem(enemy);
            if (pool.CountActive == 0)
                EventManager.Active(EEventType.PlayNextWave);
        }
    }
}