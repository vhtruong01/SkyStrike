using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : Menu, IElementContainer<WaveDataObserver>
        {
            [SerializeField] private Button addBtn;
            [SerializeField] private Button removeBtn;
            [SerializeField] private Button duplicateBtn;
            [SerializeField] private Button moveLeftBtn;
            [SerializeField] private Button moveRightBtn;
            private LevelDataObserver levelDataObserver;
            private WaveItemList waveUIGroupPool;
            private WaveDataObserver waveDataObserver;

            public override void Awake()
            {
                base.Awake();
                waveUIGroupPool = gameObject.GetComponent<WaveItemList>();
                removeBtn.onClick.AddListener(() => waveUIGroupPool.RemoveSelectedItem());
                addBtn.onClick.AddListener(waveUIGroupPool.CreateEmptyItem);
                duplicateBtn.onClick.AddListener(waveUIGroupPool.DuplicateSelectedItem);
                moveLeftBtn.onClick.AddListener(waveUIGroupPool.MoveLeftSelectedItem);
                moveRightBtn.onClick.AddListener(waveUIGroupPool.MoveRightSelectedItem);
                EventManager.onSelectLevel.AddListener(SelectLevel);
            }
            public override void Init() => waveUIGroupPool.Init(EventManager.SelectWave);
            protected override void CreateObject(ObjectDataObserver data) => waveDataObserver.Add(data);
            protected override void SelectObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) => waveDataObserver.Remove(data);
            protected override void SelectWave(WaveDataObserver data) => waveDataObserver = data;
            protected void SelectLevel(LevelDataObserver data)
            {
                levelDataObserver = data;
                waveUIGroupPool.DisplayDataList();
            }
            public IDataList<WaveDataObserver> GetDataList() => levelDataObserver;
        }
    }
}