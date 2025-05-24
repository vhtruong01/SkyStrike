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
            removeBtn.onClick.AddListener(RemoveState);
            moveUpBtn.onClick.AddListener(group.MoveLeftSelectedItem);
            moveDownBtn.onClick.AddListener(group.MoveRightSelectedItem);
            infoMenu.Hide();
            Hide();
        }
        private void RemoveState()
        {
            if (group.RemoveSelectedItem())
                SelectState(null);
        }
        private void SelectState(BulletStateDataObserver stateData)
        {
            infoMenu.Display(stateData);
            infoMenu.Show();
        }
        public void SelectBullet(BulletDataObserver data)
        {
            group.DisplayDataList(data);
            group.SelectAndInvokeItem(null);
        }
    }
}