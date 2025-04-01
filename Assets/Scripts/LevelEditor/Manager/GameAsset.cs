using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class GameAsset : MonoBehaviour
        {
            [SerializeField] private List<MetaData> metaDataList;
            private Dictionary<int, MetaData> metaDataDict;

            public void Awake()
            {
                metaDataDict = new();
                foreach (var item in metaDataList)
                    metaDataDict.Add(item.id, item);
                EventManager.onGetMetaData.AddListener(GetMetaData);
            }
            private MetaData GetMetaData(int id) => metaDataDict[id];
        }
    }
}