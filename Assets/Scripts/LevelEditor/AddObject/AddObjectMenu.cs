using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
                itemUIGroupPool.selectDataCall = EventManager.SelectMetaObject;
                foreach (var data in metaDataList)
                    itemUIGroupPool.CreateItem(out AddObjectItemUI item, data);
                for (int i = 0; i < selectObjectTypeBtn.Count; i++)
                {
                    //onclick
                }
            }
            public void Start()
            {
                selectObjectTypeBtn.SelectFirstItem(); 
            }
        }
    }
}