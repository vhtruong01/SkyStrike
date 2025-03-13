using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : Menu
        {
            [SerializeField] private Button addWaveBtn;
            [SerializeField] private Button removeWaveBtn;
            [SerializeField] private Button duplicateWaveBtn;
            [SerializeField] private Button moveLeftWaveBtn;
            [SerializeField] private Button moveRightWaveBtn;
            [SerializeField] private UIGroupPool waveUIGroupPool;
            [SerializeField] private int minElement;
            private LevelDataObserver levelDataObserver;
            private int currentWaveNameIndex;

            public override void Awake()
            {
                base.Awake();
                duplicateWaveBtn.interactable = false;
                duplicateWaveBtn.onClick.AddListener(DuplicateWave);
                addWaveBtn.onClick.AddListener(CreateWave);
                removeWaveBtn.onClick.AddListener(RemoveWave);
                moveLeftWaveBtn.onClick.AddListener(MoveLeftWave);
                moveRightWaveBtn.onClick.AddListener(MoveRightWave);
                waveUIGroupPool.selectDataCall = EventManager.SelectWave;
            }
            public override void Init()
            {
                levelDataObserver = EventManager.GetLevel() as LevelDataObserver;
                for (int i = 0; i < minElement; i++)
                    CreateWave();
                waveUIGroupPool.SelectFirstItem();
            }
            protected override void CreateObject(IEditorData data)
            {
                var waveData = waveUIGroupPool.GetSelectedItemComponent<UIElement>().data as WaveDataObserver;
                waveData?.AddObject(data as ObjectDataObserver);
            }
            public void CreateWave()
            {
                waveUIGroupPool.CreateItem(out WaveItemUI wave, levelDataObserver.Create());
                wave.SetName("Wave " + ++currentWaveNameIndex);
            }
            public void RemoveWave()
            {
                var selectedWave = waveUIGroupPool.GetSelectedItem() as WaveItemUI;
                if (selectedWave != null)
                {
                    waveUIGroupPool.RemoveSelectedItem();
                    levelDataObserver.Remove(selectedWave.data as WaveDataObserver);
                }
            }
            public void MoveLeftWave()
            {
                if (waveUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    waveUIGroupPool.MoveLeftSelectedItem();
                    levelDataObserver.Swap(index - 1, index);
                }
            }
            public void MoveRightWave()
            {
                if (waveUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    waveUIGroupPool.MoveRightSelectedItem();
                    levelDataObserver.Swap(index, index + 1);
                }
            }
            public void DuplicateWave()
            {
                //
            }
            protected override void RemoveObject(IEditorData data) { }
            protected override void SelectObject(IEditorData data) { }
            protected override void SelectWave(IEditorData data) { }
        }
    }
}