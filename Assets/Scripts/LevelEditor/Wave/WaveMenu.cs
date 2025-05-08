using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent(typeof(WaveItemList))]
    public class WaveMenu : EventNotifyMenu
    {
        [SerializeField] private Button addBtn;
        [SerializeField] private Button removeBtn;
        [SerializeField] private Button duplicateBtn;
        [SerializeField] private Button moveLeftBtn;
        [SerializeField] private Button moveRightBtn;
        private WaveItemList group;
        private WaveDataObserver waveData;

        protected override void Preprocess()
        {
            base.Preprocess();
            EventManager.onSelectLevel.AddListener(SelectLevel);
            group = gameObject.GetComponent<WaveItemList>();
            group.Init(EventManager.SelectWave);
            removeBtn.onClick.AddListener(() => group.RemoveSelectedItem());
            addBtn.onClick.AddListener(group.CreateEmptyItem);
            duplicateBtn.onClick.AddListener(group.DuplicateSelectedItem);
            moveLeftBtn.onClick.AddListener(group.MoveLeftSelectedItem);
            moveRightBtn.onClick.AddListener(group.MoveRightSelectedItem);
        }
        protected override void SelectObject(ObjectDataObserver data) { }
        protected override void CreateObject(ObjectDataObserver data) => waveData.Add(data);
        protected override void RemoveObject(ObjectDataObserver data) => waveData.Remove(data);
        protected override void SelectWave(WaveDataObserver data) => waveData = data;
        protected void SelectLevel(LevelDataObserver data)
            => group.DisplayDataList(data);
    }
}