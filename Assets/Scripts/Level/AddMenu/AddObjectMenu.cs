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

            public override void Awake()
            {
                base.Awake();
                foreach (var data in enemyMetaDataList)
                {
                    ItemUI itemUI = itemUIGroup.CreateItem<ItemUI>();
                    itemUI.onSelect.AddListener(SelectItem);
                    itemUI.data = data;
                }
            }
            public void SelectItem(ItemUI itemUI)
            {
                itemUIGroup.SelectItem(itemUI);
                MenuManager.SelectItemUI(itemUI?.data);
            }
            public override void HandleCollapse()
            {
                base.HandleCollapse();
                SelectItem(null);
            }
        }
    }
}