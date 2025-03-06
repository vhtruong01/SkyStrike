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
            private int currentWaveNameIndex;

            public override void Awake()
            {
                base.Awake();
                duplicateWaveBtn.interactable = false;
                duplicateWaveBtn.onClick.AddListener(DuplicateWave);
                addWaveBtn.onClick.AddListener(CreateWave);
                removeWaveBtn.onClick.AddListener(RemoveWave);
                moveLeftWaveBtn.onClick.AddListener(waveUIGroupPool.MoveLeftSelectedItem);
                moveRightWaveBtn.onClick.AddListener(waveUIGroupPool.MoveRightSelectedItem);
                waveUIGroupPool.selectDataCall = MenuManager.SelectWave;
            }
            public void Start()
            {
                for (int i = 0; i < minElement; i++)
                    CreateWave();
                waveUIGroupPool.SelectFirstItem();
            }
            public void CreateWave()
            {
                waveUIGroupPool.CreateItem(out WaveUI wave);
                wave.SetData(MenuManager.CreateWave());
                wave.SetName("Wave " + ++currentWaveNameIndex);
            }
            public void RemoveWave()
            {
                var selectedWave = waveUIGroupPool.GetSelectedItem() as WaveUI;
                if (selectedWave != null)
                {
                    waveUIGroupPool.RemoveSelectedItem();
                    MenuManager.RemoveWave(selectedWave.waveDataObserver);
                }
            }
            public void DuplicateWave()
            {
                //
            }
        }
    }
}