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
                waveUIGroupPool.selectDataCall = MenuManager.SelectWave;
            }
            public void Start()
            {
                levelDataObserver = MenuManager.GetLevel() as LevelDataObserver;
                for (int i = 0; i < minElement; i++)
                    CreateWave();
                waveUIGroupPool.SelectFirstItem();
            }
            public void CreateWave()
            {
                waveUIGroupPool.CreateItem(out WaveItemUI wave);
                wave.SetData(levelDataObserver.Create());
                wave.SetName("Wave " + ++currentWaveNameIndex);
            }
            public void RemoveWave()
            {
                var selectedWave = waveUIGroupPool.GetSelectedItem() as WaveItemUI;
                if (selectedWave != null)
                {
                    waveUIGroupPool.RemoveSelectedItem();
                    levelDataObserver.Remove(selectedWave.waveDataObserver);
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
        }
    }
}