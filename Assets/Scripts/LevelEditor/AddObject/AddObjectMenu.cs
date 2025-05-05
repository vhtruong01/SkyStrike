using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent(typeof(ObjectItemList))]
    public class AddObjectMenu : EventNotifyMenu
    {
        [SerializeField] private List<EnemyMetaData> metaDataList;
        [SerializeField] private List<EnemyMetaData> bossMetaDataList;
        [SerializeField] private UIGroup selectObjectTypeBtn;
        [SerializeField] private Menu hierarchyMenu;
        [SerializeField] private Button hierarchyBtn;
        private Dictionary<int, EnemyMetaData> metaDataDict;
        private ObjectItemList objectItemUIGroupPool;
        private int curSubmenuIndex;

        private EnemyMetaData GetMetaData(int id) => metaDataDict.GetValueOrDefault(id);
        protected override void Preprocess()
        {
            base.Preprocess();
            metaDataDict = new();
            foreach (var item in metaDataList)
                metaDataDict.Add(item.id, item);
            foreach (var item in bossMetaDataList)
                metaDataDict.Add(item.id, item);
            EventManager.onGetMetaData.AddListener(GetMetaData);
            hierarchyBtn.onClick.AddListener(() =>
            {
                Hide();
                hierarchyMenu.Show();
            });
            curSubmenuIndex = -1;
            objectItemUIGroupPool = gameObject.GetComponent<ObjectItemList>();
            objectItemUIGroupPool.Init(SelectMetaObject, DuplicateObject);
        }
        public void Start()
            => selectObjectTypeBtn.AddListener(SelectObjectType);
        private void SelectMetaObject(ObjectDataObserver objectDataObserver)
        {
            objectDataObserver?.ResetData();
            EventManager.SelectMetaObject(objectDataObserver);
        }
        private void DuplicateObject(ObjectDataObserver objectData)
            => EventManager.CreateObject(objectData.Clone());
        private void SelectObjectType(int index)
        {
            if (curSubmenuIndex != index)
            {
                objectItemUIGroupPool.Clear();
                var list = index == 0 ? metaDataList : bossMetaDataList;
                foreach (var metaData in list)
                {
                    ObjectDataObserver objectDataObserver = new();
                    objectDataObserver.metaData.OnlySetData(metaData);
                    objectItemUIGroupPool.CreateItem(objectDataObserver);
                }
                curSubmenuIndex = index;
            }
        }
        protected override void CreateObject(ObjectDataObserver data) { }
        protected override void RemoveObject(ObjectDataObserver data) { }
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data != null)
                objectItemUIGroupPool.SelectNone();
        }
        protected override void SelectWave(WaveDataObserver data)
            => objectItemUIGroupPool.SelectNone();
    }
}