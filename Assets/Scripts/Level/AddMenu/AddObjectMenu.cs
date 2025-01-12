using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectMenu : Menu
        {
            [SerializeField] private ItemUI itemUIPrefab;
            [SerializeField] private EnemyMetaDataGroup enemyDataGroup;
            [SerializeField] private GameObject content;
            private ItemUI selectedItem;

            public override void Awake()
            {
                base.Awake();
                foreach (var data in enemyDataGroup.enemiesData)
                {
                    var itemUI = Instantiate(itemUIPrefab, content.transform, false);
                    itemUI.onSelect.AddListener(SelectItem);
                    itemUI.data = data;
                }
            }
            public void SelectItem(ItemUI itemUI)
            {
                selectedItem?.Deactive();
                selectedItem = itemUI;
                selectedItem.Active();
                MenuManager.SelectEnemy(selectedItem.data);
            }
        }
    }
}