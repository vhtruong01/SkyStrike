using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class BulletEditor : Menu, IElementContainer<BulletDataObserver>
    {
        [SerializeField] private GameObject reviewScreen;
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private Button addBtn;
        [SerializeField] private Button removeBtn;
        [SerializeField] private Button duplicateBtn;
        [SerializeField] private BulletInfoMenu bulletInfoMenu;
        private BulletItemList bulletUIGroupPool;
        private LevelDataObserver levelDataObserver;

        public void OnEnable()
            => reviewScreen.SetActive(true);
        public override void Hide()
        {
            base.Hide();
            reviewScreen.SetActive(false);
        }
        public override void Init()
        {
            EventManager.onSelectLevel.AddListener(SelectLevel);
            bulletUIGroupPool = GetComponent<BulletItemList>();
            bulletUIGroupPool.Init(SelectBullet);
            removeBtn.onClick.AddListener(RemoveBullet);
            addBtn.onClick.AddListener(bulletUIGroupPool.CreateEmptyItem);
            duplicateBtn.onClick.AddListener(bulletUIGroupPool.DuplicateSelectedItem);
        }
        private void RemoveBullet()
        {
            bulletUIGroupPool.RemoveSelectedItem();
            SelectBullet(null);
        }
        private void SelectBullet(BulletDataObserver bulletData)
        {
            bulletInfoMenu.Display(bulletData);
            bulletInfoMenu.Show();
            bulletSpawner.ChangeBulletSpawner(bulletData);
        }
        protected void SelectLevel(LevelDataObserver data)
        {
            levelDataObserver = data;
            bulletUIGroupPool.DisplayDataList();
        }
        public IDataList<BulletDataObserver> GetDataList() => levelDataObserver;
    }
}