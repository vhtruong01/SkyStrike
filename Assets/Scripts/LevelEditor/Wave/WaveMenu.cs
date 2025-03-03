using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : Menu
        {
            [SerializeField] private Button addWaveBtn;
            [SerializeField] private UIGroupPool waveUIGroupPool;
            [SerializeField] private int minElement;

            public override void Start()
            {
                base.Start();
                for (int i = 0; i < minElement; i++)
                    CreateWave();
                addWaveBtn.onClick.AddListener(CreateWave);
            }
            public void CreateWave()
            {
                waveUIGroupPool.CreateItem(out WaveUI wave);
                wave.SetData(MenuManager.CreateWave());
                wave.SetIndex(waveUIGroupPool.Count);
            }
        }
    }
}