using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : Menu, IArrangeable
        {
            [SerializeField] private Button addWaveBtn;
            [SerializeField] private Button removeWaveBtn;
            [SerializeField] private Button duplicateWaveBtn;
            [SerializeField] private Button moveLeftWaveBtn;
            [SerializeField] private Button moveRightWaveBtn;
            [SerializeField] private UIGroupPool waveUIGroupPool;
            private int currentWaveNameIndex;
            private LevelDataObserver levelDataObserver;
            private WaveDataObserver waveDataObserver;

            public override void Awake()
            {
                base.Awake();
                addWaveBtn.onClick.AddListener(() => Create());
                duplicateWaveBtn.onClick.AddListener(Duplicate);
                removeWaveBtn.onClick.AddListener(Remove);
                moveLeftWaveBtn.onClick.AddListener(MoveLeft);
                moveRightWaveBtn.onClick.AddListener(MoveRight);
                waveUIGroupPool.selectDataCall = EventManager.SelectWave;
            }
            public override void Init()
            {
                levelDataObserver = EventManager.GetLevel() as LevelDataObserver;
                Create();
                waveUIGroupPool.SelectFirstItem();
            }
            protected override void CreateObject(IEditorData data) => waveDataObserver.Add(data as ObjectDataObserver);
            protected override void SelectObject(IEditorData data) { }
            protected override void RemoveObject(IEditorData data) => waveDataObserver.Remove(data as ObjectDataObserver);
            protected override void SelectWave(IEditorData data) => waveDataObserver = data as WaveDataObserver;
            public void Create(WaveDataObserver waveData)
            {
                if (waveData != null)
                    levelDataObserver.Add(waveData);
                else waveData = levelDataObserver.CreateEmpty();
                waveData.name.SetData("Wave " + ++currentWaveNameIndex);
                waveUIGroupPool.CreateItem(waveData);
            }
            public void Create() => Create(null);
            public void Remove()
            {
                if (waveUIGroupPool.CanRemoveSelectedIndex(out int curIndex))
                {
                    levelDataObserver.Remove(curIndex);
                    waveUIGroupPool.RemoveItem(curIndex);
                }
            }
            public void MoveLeft()
            {
                if (waveUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    levelDataObserver.Swap(index - 1, index);
                    waveUIGroupPool.MoveLeftSelectedItem();
                }
            }
            public void MoveRight()
            {
                if (waveUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    levelDataObserver.Swap(index, index + 1);
                    waveUIGroupPool.MoveRightSelectedItem();
                }
            }
            public void Duplicate() => Create(waveDataObserver.Clone());
        }
    }
}