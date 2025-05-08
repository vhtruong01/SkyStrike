using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class ObjectManager<T, V> : PoolManager<T, ObjectEntityData<V>> where T : ObjectEntity<V> where V : ObjectMetaData
    {
        [SerializeField] protected List<V> metaDataList;
        protected Dictionary<int, V> metaDataDict;

        public sealed override void Awake()
        {
            base.Awake();
            metaDataDict = new();
            foreach (var metaData in metaDataList)
                metaDataDict.Add(metaData.id, metaData);
        }
        protected void OnEnable()
            => Subcribe();
        protected void OnDisable()
            => Unsubcribe();
        protected void CreateObject(ObjectEventData<V> eventData)
        {
            if (metaDataDict.TryGetValue(eventData.metaId, out var metaData))
            {
                eventData.metaData = metaData;
                T enemy = InstantiateItem(eventData.position);
                enemy.data.SetData(eventData);
                enemy.Launch(eventData.delay);
            }
            else print("error obj");
        }
        protected abstract void Subcribe();
        protected abstract void Unsubcribe();
    }
}