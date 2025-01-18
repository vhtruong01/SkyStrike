using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private List<EnemyMetaData> enemyMetaDataList;
            [SerializeField] private ItemUI itemUIPrefab;
            [SerializeField] private GameObject content;
            private ItemUI selectedItem;

            public override void Awake()
            {
                base.Awake();
                foreach (var data in enemyMetaDataList)
                {
                    var itemUI = Instantiate(itemUIPrefab, content.transform, false);
                    itemUI.onSelect.AddListener(SelectItem);
                    itemUI.data = data;
                }
            }
            public void SelectItem(ItemUI itemUI)
            {
                if(selectedItem != null) 
                    selectedItem.Deactive();
                selectedItem = itemUI;
                selectedItem.Active();
                MenuManager.SelectEnemy(selectedItem.data);
            }
        }
    }
}