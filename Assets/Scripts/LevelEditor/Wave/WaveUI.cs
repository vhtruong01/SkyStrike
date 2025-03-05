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

            public override void Awake()
            {
                base.Awake();
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