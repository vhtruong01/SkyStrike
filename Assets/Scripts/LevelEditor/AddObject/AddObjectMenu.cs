using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private List<ObjectMetaData> metaDataList;
            [SerializeField] private UIGroupPool itemUIGroupPool;
            [SerializeField] private UIGroup selectObjectTypeBtn;
            [SerializeField] private Menu hierarchyMenu;
            [SerializeField] private Button hierarchyBtn;

            public override void Awake()
            {
                base.Awake();
                hierarchyBtn.onClick.AddListener(() =>
                {
                    Collapse();
                    hierarchyMenu.Expand();
                });
                itemUIGroupPool.selectDataCall = EventManager.SelectMetaObject;
            }
            public override void Init()
            {
                foreach (var data in metaDataList)
                {
                    ObjectDataObserver objectDataObserver = new();
                    objectDataObserver.isMetaData = true;
                    objectDataObserver.metaData.SetData(data);
                    objectDataObserver.ResetData();
                    itemUIGroupPool.CreateItem(objectDataObserver);
                }
                for (int i = 0; i < selectObjectTypeBtn.Count; i++)
                {
                    //onclick
                }
            }
            protected override void CreateObject(IEditorData data) { }
            protected override void RemoveObject(IEditorData data) { }
            protected override void SelectObject(IEditorData data) => itemUIGroupPool.SelectNone();
            protected override void SelectWave(IEditorData data) => itemUIGroupPool.SelectNone();
            public void Start()
            {
                selectObjectTypeBtn.SelectFirstItem();
            }
        }
    }
}