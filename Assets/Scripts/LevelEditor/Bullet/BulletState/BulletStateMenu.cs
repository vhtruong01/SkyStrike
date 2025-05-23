using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class BulletStateMenu : Menu
    {
        [SerializeField] private Button addBtn;
        [SerializeField] private Button removeBtn;
        [SerializeField] private Button moveUpBtn;
        [SerializeField] private Button moveDownBtn;
        [SerializeField] private BulletStateInfoMenu infoMenu;
        private BulletStateItemList group;

        protected override void Preprocess()
        {
            group = GetComponent<BulletStateItemList>();
            group.Init(SelectState);
            addBtn.onClick.AddListener(group.CreateEmptyItem);
            removeBtn.onClick.AddListener(() => group.RemoveSelectedItem());
            moveUpBtn.onClick.AddListener(group.MoveLeftSelectedItem);
            moveDownBtn.onClick.AddListener(group.MoveRightSelectedItem);
            Hide();
        }
        private void SelectState(BulletStateDataObserver stateData)
        {
            infoMenu.Display(stateData);
            infoMenu.Show();
        }
        public void SelectBullet(BulletDataObserver data)
        {
            group.DisplayDataList(data);
            SelectState(null);
        }
    }
}