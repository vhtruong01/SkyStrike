using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : EventNotifyMenu, IElementContainer<WaveDataObserver>
        {
            [SerializeField] private Button addBtn;
            [SerializeField] private Button removeBtn;
            [SerializeField] private Button duplicateBtn;
            [SerializeField] private Button moveLeftBtn;
            [SerializeField] private Button moveRightBtn;
            private LevelDataObserver levelDataObserver;
            private WaveItemList waveUIGroupPool;
            private WaveDataObserver waveDataObserver;

            public override void Init()
            {
                base.Init();
                EventManager.onSelectLevel.AddListener(SelectLevel);
                waveUIGroupPool = gameObject.GetComponent<WaveItemList>();
                waveUIGroupPool.Init(EventManager.SelectWave);
                removeBtn.onClick.AddListener(() => waveUIGroupPool.RemoveSelectedItem());
                addBtn.onClick.AddListener(waveUIGroupPool.CreateEmptyItem);
                duplicateBtn.onClick.AddListener(waveUIGroupPool.DuplicateSelectedItem);
                moveLeftBtn.onClick.AddListener(waveUIGroupPool.MoveLeftSelectedItem);
                moveRightBtn.onClick.AddListener(waveUIGroupPool.MoveRightSelectedItem);
            }
            protected override void CreateObject(ObjectDataObserver data) => waveDataObserver.Add(data);
            protected override void SelectObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) => waveDataObserver.Remove(data);
            protected override void SelectWave(WaveDataObserver data) => waveDataObserver = data;
            protected void SelectLevel(LevelDataObserver data)
            {
                levelDataObserver = data;
                waveUIGroupPool.DisplayDataList();
            }
            //public override void Restore()
            //{
            //    base.Restore();
            //    waveUIGroupPool.SelectItem(PlayerPrefs.GetInt(GetType().Name + ".index", 0));
            //}
            //public override void SaveSetting()
            //{
            //    base.SaveSetting();
            //    PlayerPrefs.SetInt(GetType().Name + ".index", waveUIGroupPool.GetSelectedItemIndex());
            //}
            public IDataList<WaveDataObserver> GetDataList() => levelDataObserver;
        }
    }
}