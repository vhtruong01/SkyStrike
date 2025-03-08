using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI waveName;
            public WaveDataObserver waveDataObserver { get; private set; }

            public void SetName(string name) => waveName.text = name;
            public override void SetData(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
            }
            public override void RemoveData() => waveDataObserver = null;
            public override IData GetData() => waveDataObserver;
        }
    }
}