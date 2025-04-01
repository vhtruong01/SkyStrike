using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private List<MetaData> metaDataList;
            [SerializeField] private UIGroup selectObjectTypeBtn;
            [SerializeField] private Menu hierarchyMenu;
            [SerializeField] private Button hierarchyBtn;
            private ObjectItemList objectItemUIGroupPool;

            public override void Awake()
            {
                base.Awake();
                hierarchyBtn.onClick.AddListener(() =>
                {
                    Hide();
                    hierarchyMenu.Show();
                });
            }
            public override void Init()
            {
                objectItemUIGroupPool = gameObject.GetComponent<ObjectItemList>();
                objectItemUIGroupPool.Init(SelectMetaObject);
                foreach (var metaData in metaDataList)
                {
                    ObjectDataObserver objectDataObserver = new();
                    objectDataObserver.metaData.SetData(metaData);
                    objectDataObserver.ResetData();
                    objectItemUIGroupPool.CreateItem(objectDataObserver);
                }
                for (int i = 0; i < selectObjectTypeBtn.Count; i++)
                    selectObjectTypeBtn.GetBaseItem(i).onSelectUI.AddListener(SelectObjectType);
                selectObjectTypeBtn.SelectFirstItem();
            }
            private void SelectMetaObject(ObjectDataObserver objectDataObserver)
            {
                objectDataObserver?.ResetData();
                EventManager.SelectMetaObject(objectDataObserver);
            }
            private void SelectObjectType(int index)
            {
                if (objectItemUIGroupPool.GetSelectedItemIndex() != index)
                {

                    //
                }
            }
            protected override void CreateObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) { }
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (data != null)
                    objectItemUIGroupPool.SelectNone();
            }
            protected override void SelectWave(WaveDataObserver data) => objectItemUIGroupPool.SelectNone();
        }
    }
}