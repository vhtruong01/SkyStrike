using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class GameAsset : MonoBehaviour
        {
            [SerializeField] private List<ObjectMetaData> metaDataList;
            private Dictionary<int, ObjectMetaData> metaDataDict;

            public void Awake()
            {
                metaDataDict = new();
                foreach (var item in metaDataList)
                    metaDataDict.Add(item.id, item);
                EventManager.onGetMetaData.AddListener(GetMetaData);
            }
            private ObjectMetaData GetMetaData(int id) => metaDataDict[id];
        }
    }
}