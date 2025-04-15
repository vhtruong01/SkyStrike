using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
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

            public override void Awake()
            {
                base.Awake();
                EventManager.onSelectLevel.AddListener(SelectLevel);
            }
            public override void Show()
            {
                base.Show();
                reviewScreen.SetActive(gameObject.activeSelf);
            }
            public override void Hide()
            {
                base.Hide();
                reviewScreen.SetActive(gameObject.activeSelf);
            }
            public override void Init()
            {
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
}