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
            private int currentWaveIndex;

            public override void Start()
            {
                base.Start();
                addWaveBtn.onClick.AddListener(CreateWave);
                duplicateWaveBtn.onClick.AddListener(DuplicateWave);
                removeWaveBtn.onClick.AddListener(waveUIGroupPool.RemoveSelectedItem);
                moveLeftWaveBtn.onClick.AddListener(waveUIGroupPool.MoveLeftSelectedItem);
                moveRightWaveBtn.onClick.AddListener(waveUIGroupPool.MoveRightSelectedItem);
                for (int i = 0; i < minElement; i++)
                    CreateWave();
                waveUIGroupPool.SelectFirstItem();
            }
            public void CreateWave()
            {
                waveUIGroupPool.CreateItem(out WaveUI wave);
                wave.SetData(MenuManager.CreateWave());
                wave.SetName("Wave " + ++currentWaveIndex);
            }
            public void DuplicateWave() { print("comming soon..."); }
        }
    }
}