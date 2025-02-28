using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private UIGroupPool itemUIGroupPool;
            [SerializeField] private UIGroup selectObjectTypeBtn;
            [SerializeField] private List<EnemyMetaData> enemyMetaDataList;

            public void Start()
            {
                foreach (var data in enemyMetaDataList)
                {
                    itemUIGroupPool.CreateItem(out ItemUI itemUI);
                    itemUI.SetData(data);
                }
                for (int i = 0; i < selectObjectTypeBtn.Count; i++)
                {
                    //
                }
                selectObjectTypeBtn.SelectFirstItem();
            }
        }
    }
}