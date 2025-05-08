using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent(typeof(ObjectItemList))]
    public class AddObjectMenu : EventNotifyMenu
    {
        [SerializeField] private List<ObjectMetaData> enemyList;
        [SerializeField] private List<ObjectMetaData> bossList;
        [SerializeField] private List<ObjectMetaData> objectList;
        [SerializeField] private UIGroup selectObjectTypeBtn;
        [SerializeField] private Menu hierarchyMenu;
        [SerializeField] private Button hierarchyBtn;
        private Dictionary<int, ObjectMetaData> metaDataDict;
        private ObjectItemList group;
        private int curSubmenuIndex;

        private ObjectMetaData GetMetaData(int id) => metaDataDict.GetValueOrDefault(id);
        protected override void Preprocess()
        {
            base.Preprocess();
            metaDataDict = new();
            foreach (var item in enemyList)
                metaDataDict.Add(item.id, item);
            foreach(var item in objectList)
                metaDataDict.Add(item.id, item);
            foreach (var item in bossList)
                metaDataDict.Add(item.id, item);
            EventManager.onGetMetaData.AddListener(GetMetaData);
            hierarchyBtn.onClick.AddListener(() =>
            {
                Hide();
                hierarchyMenu.Show();
            });
            curSubmenuIndex = -1;
            group = gameObject.GetComponent<ObjectItemList>();
            group.Init(SelectMetaObject, DuplicateObject);
        }
        public void Start()
            => selectObjectTypeBtn.AddListener(SelectObjectType);
        private void SelectMetaObject(ObjectDataObserver objectData)
        {
            objectData?.ResetData();
            EventManager.SelectMetaObject(objectData);
        }
        private void DuplicateObject(ObjectDataObserver objectData)
            => EventManager.CreateObject(objectData.Clone());
        private void SelectObjectType(int index)
        {
            if (curSubmenuIndex != index)
            {
                group.Clear();
                List<ObjectMetaData> list;
                if (index == 0)
                    list = enemyList;
                else if (index == 1)
                    list = objectList;
                else list = bossList;
                foreach (var metaData in list)
                {
                    ObjectDataObserver objectData = new();
                    objectData.metaData.OnlySetData(metaData);
                    group.CreateItem(objectData);
                }
                curSubmenuIndex = index;
            }
        }
        protected override void CreateObject(ObjectDataObserver data) { }
        protected override void RemoveObject(ObjectDataObserver data) { }
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data != null)
                group.SelectNone();
        }
        protected override void SelectWave(WaveDataObserver data)
            => group.SelectNone();
    }
}