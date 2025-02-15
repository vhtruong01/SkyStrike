using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private List<EnemyMetaData> enemyMetaDataList;
            [SerializeField] private UIGroup itemUIGroup;

            public void Start()
            {
                foreach (var data in enemyMetaDataList)
                {
                    ItemUI itemUI = itemUIGroup.CreateItem<ItemUI>();
                    itemUI.Init(data, SelectItem);
                }
            }
            public void SelectItem(ItemUI itemUI)
            {
                itemUIGroup.SelectItem(itemUI?.gameObject);
                MenuManager.SelectItemUI(itemUI?.enemyDataObserver);
            }
            public override void HandleCollapse()
            {
                base.HandleCollapse();
                SelectItem(null);
            }
        }
    }
}