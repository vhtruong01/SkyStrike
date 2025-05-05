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
        private WaveItemList waveUIGroupPool;
        private WaveDataObserver waveDataObserver;

        protected override void Preprocess()
        {
            base.Preprocess();
            EventManager.onSelectLevel.AddListener(SelectLevel);
            waveUIGroupPool = gameObject.GetComponent<WaveItemList>();
            waveUIGroupPool.Init(EventManager.SelectWave);
            removeBtn.onClick.AddListener(() => waveUIGroupPool.RemoveSelectedItem());
            addBtn.onClick.AddListener(waveUIGroupPool.CreateEmptyItem);
            duplicateBtn.onClick.AddListener(waveUIGroupPool.DuplicateSelectedItem);
            moveLeftBtn.onClick.AddListener(waveUIGroupPool.MoveLeftSelectedItem);
            moveRightBtn.onClick.AddListener(waveUIGroupPool.MoveRightSelectedItem);
        }
        protected override void SelectObject(ObjectDataObserver data) { }
        protected override void CreateObject(ObjectDataObserver data) => waveDataObserver.Add(data);
        protected override void RemoveObject(ObjectDataObserver data) => waveDataObserver.Remove(data);
        protected override void SelectWave(WaveDataObserver data) => waveDataObserver = data;
        protected void SelectLevel(LevelDataObserver data)
            => waveUIGroupPool.DisplayDataList(data);
    }
}