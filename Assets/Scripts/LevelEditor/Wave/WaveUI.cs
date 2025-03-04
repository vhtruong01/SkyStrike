using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI waveName;
            public WaveDataObserver waveDataObserver { get; private set; }

            public void Start()
            {
                onClick.AddListener(() =>
                {
                    if (waveDataObserver != null)
                        MenuManager.SelectWave(waveDataObserver);
                });
            }
            public void SetName(string name) => waveName.text = name;
            public override void SetData(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
                //
            }
            public override void RemoveData()
            {
                MenuManager.RemoveWave(waveDataObserver);
                waveDataObserver = null;
            }
        }
    }
}