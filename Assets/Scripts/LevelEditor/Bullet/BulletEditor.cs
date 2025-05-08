using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class BulletEditor : Menu
    {
        [SerializeField] private GameObject reviewScreen;
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private Button addBtn;
        [SerializeField] private Button removeBtn;
        [SerializeField] private Button duplicateBtn;
        [SerializeField] private BulletInfoMenu bulletInfoMenu;
        private BulletItemList group;

        public void OnEnable()
            => reviewScreen.SetActive(true);
        public override void Hide()
        {
            base.Hide();
            reviewScreen.SetActive(false);
        }
        protected override void Preprocess()
        {
            EventManager.onSelectLevel.AddListener(SelectLevel);
            group = GetComponent<BulletItemList>();
            group.Init(SelectBullet);
            removeBtn.onClick.AddListener(RemoveBullet);
            addBtn.onClick.AddListener(group.CreateEmptyItem);
            duplicateBtn.onClick.AddListener(group.DuplicateSelectedItem);
        }
        private void RemoveBullet()
        {
            group.RemoveSelectedItem();
            SelectBullet(null);
        }
        private void SelectBullet(BulletDataObserver bulletData)
        {
            bulletInfoMenu.Display(bulletData);
            bulletInfoMenu.Show();
            bulletSpawner.ChangeBulletSpawner(bulletData);
        }
        protected void SelectLevel(LevelDataObserver data)
            => group.DisplayDataList(data);
    }
}