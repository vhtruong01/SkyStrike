using UnityEngine;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private UIGroupPool itemUIGroupPool;
            [SerializeField] private UIGroup selectObjectTypeBtn;
            [SerializeField] private List<EnemyMetaData> enemyMetaDataList;

            public override void Awake()
            {
                base.Awake();
                itemUIGroupPool.selectDataCall = MenuManager.SelectItemUI;
            }
            public void Start()
            {
                foreach (var data in enemyMetaDataList)
                {
                    itemUIGroupPool.CreateItem(out AddObjectItemUI itemUI);
                    itemUI.SetData(data);
                }
                for (int i = 0; i < selectObjectTypeBtn.Count; i++)
                {
                    //onclick
                }
                selectObjectTypeBtn.SelectFirstItem();
            }
        }
    }
}