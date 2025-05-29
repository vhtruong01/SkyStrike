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
        [SerializeField] private Button stateBtn;
        [SerializeField] private BulletInfoMenu bulletInfoMenu;
        [SerializeField] private BulletStateMenu bulletStateMenu;
        [SerializeField] private Slider slider;
        private BulletItemList group;

        public void OnEnable()
        {
            group?.SelectNone();
            SelectBullet(null);
            reviewScreen.SetActive(true);
        }
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
            stateBtn.onClick.AddListener(DisplayStateMenu);
            slider.onValueChanged.AddListener(val => reviewScreen.transform.localScale = val * Vector3.one);
            slider.value = slider.minValue;
        }
        private void DisplayStateMenu()
        {
            if (bulletStateMenu.gameObject.activeSelf)
                bulletStateMenu.Hide();
            else bulletStateMenu.Show();
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
            bulletStateMenu.SelectBullet(bulletData);
            bulletSpawner.ChangeBulletSpawner(bulletData);
            stateBtn.gameObject.SetActive(bulletData != null);
        }
        protected void SelectLevel(LevelDataObserver data)
            => group.DisplayDataList(data);
    }
}